using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using beltexam.Models;

namespace beltexam.Controllers
{
    public class HomeController : Controller
    {
        private beltexamContext _dbcontext;

        //Constructor
        public HomeController(beltexamContext context)
        {
            _dbcontext = context;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
             return RedirectToAction("Register");
        }

        [HttpPost]
        [HttpGet]
        [Route("register")]
        public IActionResult Register(RegisterViewModel model)
        {
            if(ModelState.IsValid) 
            {
                HttpContext.Session.SetString("email", model.Email);
                HttpContext.Session.SetString("firstname", model.FirstName);

                var user = new User
                {
                    Firstname = model.FirstName,
                    Lastname = model.LastName,
                    Email = model.Email,
                    Password = model.Password,
                    CreatedAt = DateTime.UtcNow
                };
                
                _dbcontext.Users.Add(user);
                _dbcontext.SaveChanges();

                return RedirectToAction("Dashboard", "DojoActivity"); 
            }
            return View(model);
        }

        [HttpGet]
        [HttpPost]
        [Route("login")]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _dbcontext.Users.SingleOrDefault(dbUser => dbUser.Email == model.Email && dbUser.Password == model.Password);
                
                if (user != null)
                {
                    HttpContext.Session.SetString("email", model.Email);
                    HttpContext.Session.SetString("firstname", user.Firstname);
                    
                    return RedirectToAction("Dashboard", "DojoActivity");
                } else
                {
                    ViewBag.UserOrPasswordIsWrong = "Username or password is not valid";
                }
            }
            return View(model);
        }

        [HttpGet]
        [Route("logoff")]
        public IActionResult Logoff()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index"); 
        }
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
