using RBAC.Data.DTOs;
using RBAC.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RBAC.Service.Implementation
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _repository;

        public AdminService(IAdminRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<UserDto>> GetAllUsersAsync()
        {
            var users = await _repository.GetAllUsersAsync();

            return users.Select(u => new UserDto
            {
                Id = u.Id,
                Username = u.Username,
                Email = u.Email,
                Roles = u.UserRoles.Select(ur => ur.Role.Name).ToList()
            }).ToList();
        }

        public async Task AssignRoleAsync(int userId, int roleId)
        {
            await _repository.AssignRoleAsync(userId, roleId);
        }
    }

}
