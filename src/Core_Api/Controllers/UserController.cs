using Core_Api.Commons;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.DTOs;
using Service;
using Service.commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core_Api.Controllers
{
    [Authorize(Roles = RolesHelper.Admin)]
    [Route("users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<DataCollection<ApplicationUserDTO>>> GetAll(int page, int take)

        {
            // return Ok(new { msg = "user controllers" });
            return await _userService.GetAll(page, take);
        }
    }
}
