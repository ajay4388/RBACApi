using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RBAC.Data.DTOs
{
    public class Register
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } = "Viewer"; // Default role
    }
}
