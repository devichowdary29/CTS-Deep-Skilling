using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace JwtAuthApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private string GenerateJSONWebToken(int userId, string userRole)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("mysuperdupersecret_123456789012"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Role, userRole),
                new Claim("UserId", userId.ToString())
            };

            var token = new JwtSecurityToken(
                        issuer: "mySystem",
                        audience: "myUsers",
                        claims: claims,
                        expires: DateTime.Now.AddMinutes(2), // Lab 5 Requirement: Expire in 2 minutes
                        signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpGet("login")]
        public IActionResult Login(int userId = 1, string role = "Admin")
        {
            var token = GenerateJSONWebToken(userId, role);
            return Ok(new { token });
        }
    }
}
