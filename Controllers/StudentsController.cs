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
            return View(students);

        }
        // web page to add students 
        [Authorize]
        public IActionResult Add()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
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
