using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmallRatings.Business;
using SmallRatings.Models;

namespace EmployeeCrud.Controllers
{
    public class UserController : Controller
    {
        CommWithDataAccess userDAL = new CommWithDataAccess();

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        //The basic purpose of ValidateAntiForgeryToken is to prevent cross-site request forgery attacks.
        [ValidateAntiForgeryToken]
        // The Bind attribute is one way to protect against over-posting.
        public IActionResult Register([Bind]UserInfo obj)
        {
            //Make sure the data is valid (another form of validation
            if(ModelState.IsValid)
            {
                if (userDAL.AddUser(obj))
                {
                    TempData["RegMessage"] = "Registration Successful. Navigate to Navbar to Login!";
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(obj);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        //The basic purpose of ValidateAntiForgeryToken is to prevent cross-site request forgery attacks.
        [ValidateAntiForgeryToken]
        // The Bind attribute is one way to protect against over-posting.
        public IActionResult Login([Bind] LoginInfo obj)
        {
            //Make sure the data is valid (another form of validation
            if (ModelState.IsValid)
            {
                if (userDAL.LoginUser(obj))
                {
                    return RedirectToAction("Index", "User");
                }
            }
            return View(obj);
        }

    }
}
