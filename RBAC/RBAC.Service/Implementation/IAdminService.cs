using RBAC.Data.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RBAC.Service.Implementation
{
    public interface IAdminService
    {
        Task<List<UserDto>> GetAllUsersAsync();
        Task AssignRoleAsync(int userId, int roleId);
    }

}
