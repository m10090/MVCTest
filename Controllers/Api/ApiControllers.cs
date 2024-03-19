using testMvc.Data;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using testMvc.Models;
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
        public async Task<IActionResult> Login([FromBody] LoginModel login)
        {
            IActionResult response = Unauthorized();
            var db = new ApplicationDbContext();
            var user = db.Users.FirstOrDefault(x => x.username == login.Username && x.password == login.Password);
            Console.WriteLine(login.Username);
            Console.WriteLine(login.Password);
            if (user != null)
            {
              var claims = new List<Claim>
              {
                  new Claim(ClaimTypes.Name, user.username),
                  new Claim(ClaimTypes.Role, "Admin")
              };
              var Identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
              var principal = new ClaimsPrincipal(Identity);
              var authProperties = new AuthenticationProperties
              {
                  IsPersistent = true,
                  ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
              };
              await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal,authProperties);
              return Ok(new { message = "Login Success" });
            }
            return response;
        }

  }
}
