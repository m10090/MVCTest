using System.Diagnostics;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using testMvc.Models;
using testMvc.Data;
using Microsoft.AspNetCore.Authorization;


namespace testMvc.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult register()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        public async Task<IActionResult> logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
        // my code
        [HttpPut]
        [Route("User/api/register")]
        public IResult _register([FromBody] UserRegister newUser)
        {
            // could add conditions and add the use to the data base
            ApplicationDbContext db = new ApplicationDbContext();
            if (db.Users.Where((x) => x.username == newUser.username)
                .Any() || db.Users.Where((x) => x.email == newUser.email).Any())
            {
                return Results.BadRequest();
            }
            if (newUser.username == null || newUser.password == null || newUser.email == null)
            {
                return Results.BadRequest();
            }
            User user = new User
            {
                username = newUser.username,
                password = newUser.password,
                email = newUser.email
            };
            db.Users.Add(user);
            db.SaveChangesAsync();
            return Results.Ok();
        }
        // Monday task
        public string TestString()
        {
            return "login";
        }
        public int testInt()
        {
            return 0;
        }
        public EmptyResult testEmpty()
        {
            return new EmptyResult();
        }
        public JsonResult testJson()
        {
            return new JsonResult(new { name = "test" });
        }
        public NotFoundResult testNotFound()
        {
            return new NotFoundResult();
        }
        public RedirectResult testRedirect()
        {
            return new RedirectResult("https://www.google.com");
        }
        public RedirectToActionResult testRedirectToAction()
        {
            return new RedirectToActionResult("Index", "Home", null);
        }
        public IActionResult testContent(){
          return Content("hi test") ;
        }

    }
}
