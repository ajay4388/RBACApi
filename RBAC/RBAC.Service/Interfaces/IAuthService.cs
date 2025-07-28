using RBAC.Data.DTOs;
using RBAC.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RBAC.Service.Interfaces
{
    public interface IAuthService
    {
        Task<string?> LoginAsync(Login request);
        Task<bool> RegisterAsync(Register request);
        Task<List<User>> GetUsers();
    }
}
