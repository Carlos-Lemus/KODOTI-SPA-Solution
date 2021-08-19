using Core_Api.Commons;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Model.DTOs;
using Model.Indentity;
using Service;
using Service.commons;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core_Api.Controllers
{
    [ApiController]
    [Route("identity")]
    public class IdentityController: ControllerBase
    {
        private readonly UserManager<ApplicationUser> _manager;
        private readonly SignInManager<ApplicationUser> _singInManager;
        private readonly IConfiguration _configuration;

        public IdentityController(
            UserManager<ApplicationUser> manager, 
            SignInManager<ApplicationUser> singInManager,
            IConfiguration configuration
            )
        {
            this._manager = manager;
            this._singInManager = singInManager;
            this._configuration = configuration;
        }

        // identity/register
        [HttpPost("register")]
        public async Task<IActionResult> Create(ApplicationUserRegisterDTO model)
        {
            ApplicationUser user = new ApplicationUser
            {
                Email = model.Email,
                UserName = model.Email
            };

            var result = await _manager.CreateAsync(user , model.Password);

            await _manager.AddToRoleAsync(user, RolesHelper.Seller);

            if(!result.Succeeded)
            {
                throw new Exception("No se pudo crear el usuario :(");
            }

            return Ok();
        }

        // identity/login
        [HttpPost("login")]
        public async Task<IActionResult> Login(ApplicationUserLoginDTO model)
        {
            var user = await _manager.FindByEmailAsync(model.Email);
            
            if(user != null)
            {
                var check = await _singInManager.CheckPasswordSignInAsync(user, model.Password, false);
        
                if(check.Succeeded)
                {
                    return Ok(
                        await GenerateToken(user)
                        );
                } 

            }
            
            return BadRequest("El usuario o contraseña son incorrecto");
        }

        private async Task<string> GenerateToken(ApplicationUser user)
        {
            var secretKey = _configuration.GetValue<string>("SecretKey");
            var key = Encoding.ASCII.GetBytes(secretKey);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var roles = await _manager.GetRolesAsync(user);

            foreach(var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var tokenDescriptior = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                    /*new Claim[]
                    {

                        new Claim(ClaimTypes.Name, user.FirstName),
                        new Claim(ClaimTypes.Surname, user.LastName)
                    }),*/
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),

            };


            var tokenHandler = new JwtSecurityTokenHandler();
            var createdToken = tokenHandler.CreateToken(tokenDescriptior);

            return tokenHandler.WriteToken(createdToken);

        }

        [Authorize]
        [HttpGet("refresh_token")]
        public async Task<IActionResult> Refresh()
        {
            // Aquí esta el ID del usuario, lo estamos leyendo del token
            var userId = User.Claims.Where(x =>
                x.Type.Equals(ClaimTypes.NameIdentifier)
            ).Single().Value;


            var user = await _manager.FindByIdAsync(userId);


            return Ok(
                await GenerateToken(user)
            );
        }
    }
}
