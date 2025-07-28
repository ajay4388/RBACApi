using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RBAC.Data.Data;
using RBAC.Data.DTOs;
using RBAC.Data.Entities;
using RBAC.Data.Interfaces;
using RBAC.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RBAC.Service.Implementation
{
    public class AuthService:IAuthService
    {
        private readonly RBACDbContext _context;
        private readonly IConfiguration _config;
        private readonly IAuthRepository _repo;
        public AuthService(RBACDbContext context, IConfiguration config, IAuthRepository repo)
        {
            _context = context;
            _config = config;
            _repo = repo;
        }

        public async Task<bool> RegisterAsync(Register request)
        {
            request.Password = PasswordHash(request.Password);

            var result = await _repo.RegisterAsync(request);
            return result;
        }

        public async Task<List<User>> GetUsers()
        {
            var result = await _repo.GetUsers();
            return result;


        }

        public async Task<string?> LoginAsync(Login request)
        {
            var user = await _repo.LoginAsync(request);

            if (user == null || !VerifyPassword(request.Password, user.PasswordHash))
                return null;

            var roles = user.UserRoles.Select(ur => ur.Role.Name).ToList();

            return GenerateJwtToken(user, roles);
        }

        private string GenerateJwtToken(User user, List<string> roles)
        {
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
        };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private static string PasswordHash(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }

        private static bool VerifyPassword(string password, string storedHash)
        {
            return PasswordHash(password) == storedHash;
        }
    }
}
