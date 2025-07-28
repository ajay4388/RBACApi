using Azure.Core;
using Microsoft.EntityFrameworkCore;
using RBAC.Data.Data;
using RBAC.Data.DTOs;
using RBAC.Data.Entities;
using RBAC.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RBAC.Data.Implementation
{
    public class AuthRepository : IAuthRepository
    {
        private readonly RBACDbContext _context;
        public AuthRepository(RBACDbContext context)
        {
            _context = context;
        }

        public async Task<User> LoginAsync(Login request)
        {
            try
            {
                var user = await _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Username == request.Username);
                return user;
            }
            catch(Exception ex)
            {
                return null; 
            }
            //return user;
        }

        public async Task<List<User>> GetUsers()
        {
            var users = await _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .ToListAsync();

            return users;
        }

        public async Task<bool> RegisterAsync(Register register)
        {
            if (await _context.Users.AnyAsync(u => u.Username == register.Username))
                return false;

           

            var user = new User
            {
                Username = register.Username,
                PasswordHash = register.Password,
                
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var role = await _context.Roles.FirstOrDefaultAsync(r => r.Name ==register.Role);
            if (role == null) return false;

            _context.UserRoles.Add(new UserRole
            {
                UserId = user.Id,
                RoleId = role.Id
            });

            await _context.SaveChangesAsync();
            return true;
        }
        

        

        
    }

}
