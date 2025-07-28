using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RBAC.Data.DTOs;
using RBAC.Data.Entities;
using RBAC.Service.Interfaces;

namespace RBAC.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Register request)
        {
            var result = await _authService.RegisterAsync(request);
            if (!result)
                return BadRequest("Username already exists or role is invalid.");

            return Ok(new { message = "User registered successfully" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Login request)
        {
            var token = await _authService.LoginAsync(request);
            if (token == null)
                return Unauthorized("Invalid username or password.");

            return Ok(new { token });
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("users")]
        public async Task<IActionResult> GetUsers()
        {
            var result = await _authService.GetUsers();
            if (result == null)
                return Ok(new List<Data.Entities.User>());

            return Ok(result);
        }

       

    }
}
