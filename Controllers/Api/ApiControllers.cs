using Microsoft.AspNetCore.Authentication.JwtBearer;
using testMvc.Data;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using testMvc.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
namespace testMvc.Controllers.Api
{
    [Route("/api")]
    [ApiController]
    public class ApiControllers : Controller
    {
        private readonly IConfiguration _config;

        public ApiControllers(IConfiguration config)
        {
            _config = config;
        }
        [HttpPost]
        [Route("/api/login")]
        public IActionResult Login([FromBody] LoginModel login)
        {
            IActionResult response = Unauthorized();
            var db = new ApplicationDbContext();
            var user = db.Users.FirstOrDefault(x => x.username == login.Username && x.password == login.Password);
            if (user != null)
            {
                var tokenString = GenerateJSONWebToken(user.Id.ToString());
                Response.Cookies.Append("auth_token", tokenString, new CookieOptions() { HttpOnly = true });
                response = RedirectToAction("Index", "Students");
            }
            return response;
        }
        private string GenerateJSONWebToken(string username)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              null,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
