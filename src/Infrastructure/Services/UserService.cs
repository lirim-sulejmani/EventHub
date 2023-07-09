using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carmax.Application.Common.Interfaces;
using Carmax.Application.Common.Models;
using Carmax.Application.Features.User.Dtos;
using Carmax.Domain.Entities;
using Carmax.Domain.Enums;
using Carmax.Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Carmax.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IApplicationDbContext _context;

        public UserService(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string?> GetUserNameAsync(Guid userId)
        {
            var user = await _context.Users.Where(u => u.Id == userId).FirstOrDefaultAsync();
            return user.Email;
        }

        public async Task<bool> IsInRoleAsync(Guid userId, UserRole role)
        {
            var user = _context.Users.SingleOrDefault(u => u.Id == userId);
            return user != null && await _context.Users.AnyAsync(x => x.Id == userId && x.RoleId == role);
        }
    }
}
