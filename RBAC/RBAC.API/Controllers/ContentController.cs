using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RBAC.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContentController : ControllerBase
    {
        [HttpGet("ContentPublish")]
        [Authorize(Roles = "Admin")]
        public IActionResult ContentPublishApprove()
        {
            return Ok(new { message = "Only Admins can do this."});
        }

        [HttpGet("ContentCreate")]
        [Authorize(Roles = "Admin,Editor")]
        public IActionResult ContentCreateOrUpdate()
        {
            return Ok(new { message = "Admins and Editors can do this." });
        }

        [HttpGet("ViewContent")]
        [Authorize(Roles = "Admin,Editor,Viewer")]
        public IActionResult View()
        {
            return Ok(new { message = "Everyone (including Viewers) can do this." });
        }
    }
}
