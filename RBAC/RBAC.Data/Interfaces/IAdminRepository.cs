using RBAC.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RBAC.Data.Interfaces
{
    public interface IAdminRepository
    {
        Task<List<User>> GetAllUsersAsync();
        Task AssignRoleAsync(int userId, int roleId);
    }

}
