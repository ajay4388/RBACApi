using RBAC.Data.DTOs;
using RBAC.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RBAC.Data.Interfaces
{
    public interface IAuthRepository
    {
        Task<User> LoginAsync(Login request);
        Task<bool> RegisterAsync(Register request);

        Task<List<User>> GetUsers();
    }

}
