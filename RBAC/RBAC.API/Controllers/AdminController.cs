using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RBAC.Data.DTOs;
using RBAC.Service.Implementation;

namespace RBAC.API.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _service;


        public AdminController(IAdminService service)
        {
            _service = service;
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _service.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpPost("assign-role")]
        public async Task<IActionResult> AssignRole([FromBody] AssignRoleDto dto)
        {
            await _service.AssignRoleAsync(dto.UserId, dto.RoleId);
            return Ok(new { message = "Role assigned successfully" });
        }
    }

}
