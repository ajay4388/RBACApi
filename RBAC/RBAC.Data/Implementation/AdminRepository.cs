using Microsoft.EntityFrameworkCore;
using RBAC.Data.Data;
using RBAC.Data.Entities;
using RBAC.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RBAC.Data.Implementation
{
    public class AdminRepository : IAdminRepository
    {
        private readonly RBACDbContext _context;
        public AdminRepository(RBACDbContext context)
        {
            _context = context;
        }

       

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .ToListAsync();
        }

        public async Task AssignRoleAsync(int userId, int roleId)
        {
            var exists = await _context.UserRoles
                .AnyAsync(ur => ur.UserId == userId && ur.RoleId == roleId);

            if (!exists)
            {
                _context.UserRoles.Add(new UserRole { UserId = userId, RoleId = roleId });
                await _context.SaveChangesAsync();
            }
        }
    }

}
