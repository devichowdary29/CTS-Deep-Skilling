using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;
using JwtMicroservice.Models;

namespace JwtMicroservice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        // Question 1: Add a login endpoint
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            if (IsValidUser(model))
            {
                var token = GenerateJwtToken(model.Username);
                return Ok(new { Token = token });
            }
            return Unauthorized();
        }

        private bool IsValidUser(LoginModel model)
        {
            // Simple demonstration validator
            return !string.IsNullOrEmpty(model.Username) && model.Password == "password";
        }

        // Question 1: Generate a JWT token upon successful login
        private string GenerateJwtToken(string username)
        {
            // Question 3: Add roles to JWT claims
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, "Admin")
            };

            // Using padded key from appsettings to prevent crash
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ThisIsASecretKeyForJwtToken12345"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "MyAuthServer",
                audience: "MyApiUsers",
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
