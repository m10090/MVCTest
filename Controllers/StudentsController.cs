using Microsoft.AspNetCore.Mvc;
using testMvc.Data;
// include jwt 
using System.IdentityModel.Tokens.Jwt;
using testMvc.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
namespace test.Controllers
{
    public class StudentsController : Controller
    {
        // show all 
        public IActionResult Index()
        {
            var db = new ApplicationDbContext();
            List<Student> students = db.Students.ToList();
            bool isDevelopment = string.Compare(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"), "Development", StringComparison.OrdinalIgnoreCase) == 0;
            if (isDevelopment)
                students = new List<Student> {
              new Student { name = "John", email = "hi@mail.com",age = 12, degree= 40} ,
            new Student { name = "Jane", email = "jane@cow.com",age = 12, degree= 40},
            new Student { name = "omar", email = "omar@lgt.com",age = 15, degree= 90},
            new Student { name = "mohamed", email = "i2l@rt.com",age = 12, degree= 40}

          };
            return View(students);

        }
        // web page to add students 
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [Authorize]
        public IActionResult AddStudent([FromForm] Student student)
        {
            Console.WriteLine("Adding student");
            ApplicationDbContext db = new ApplicationDbContext();
            db.Students.Add(student);
            db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }

}
