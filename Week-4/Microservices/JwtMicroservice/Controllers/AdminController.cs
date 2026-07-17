using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace JwtMicroservice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        // Question 3: Add Role-Based Authorization
        [HttpGet("dashboard")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAdminDashboard()
        {
            return Ok("Welcome to the admin dashboard.");
        }
    }
}
