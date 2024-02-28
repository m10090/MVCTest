using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using testMvc.Models;

namespace testMvc.Controllers{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            
            return View();
        }
        [Authorize]
        public IActionResult Login(){
            if (User.Identity.IsAuthenticated){
                return RedirectToAction("Index", "Home");
            } 
            return View();
        }
        public IActionResult Logout(){
            
            return RedirectToAction("Index", "Home");
        }

    }
}
