using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Model.DTOs;
using Persistence.Database;
using Service.commons;
using Service.extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IUserService
    {
        Task<DataCollection<ApplicationUserDTO>> GetAll(int page, int take);
    }

    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UserService(
            ApplicationDbContext context,
            IMapper mapper
        )
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<DataCollection<ApplicationUserDTO>> GetAll(int page, int take)
        {
            return _mapper.Map<DataCollection<ApplicationUserDTO>>(
                await _context.Users.OrderByDescending(x => x.UserName)
                              .Include(x => x.UserRoles)
                              .ThenInclude(x => x.Role)
                              .AsQueryable()
                              .PageAsync(page, take)
            );
        }
    }
}
