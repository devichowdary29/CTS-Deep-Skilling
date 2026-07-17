using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace JwtMicroservice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SecureController : ControllerBase
    {
        // Question 2: Secure an endpoint using [Authorize]
        [HttpGet("data")]
        [Authorize]
        public IActionResult GetSecureData()
        {
            return Ok("This is protected data.");
        }
    }
}
