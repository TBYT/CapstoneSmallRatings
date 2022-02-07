using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmallRatings.Business;
using SmallRatings.Models;
using Microsoft.AspNetCore.Http;

namespace EmployeeCrud.Controllers
{
    public class UserController : Controller
    {
        CommWithDataAccess userDAL = new CommWithDataAccess();
        public const string SessionKeyId = "_Id";
        static UserInfo staticUser;

        [HttpGet]
        public IActionResult Index()
        {
            var UserID = HttpContext.Session.GetInt32("_Id");
            if (UserID.HasValue)
            {
                return View();
            }
            else
                return RedirectToAction("Index", "Home");
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

        [HttpGet]
        public IActionResult Logout()
        {
            //clears session so player can set new session.
            HttpContext.Session.Clear();
            staticUser = null;
            return RedirectToAction("Index", "Home");
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
                UserInfo currentUser = userDAL.LoginUser(obj);
                if (currentUser!=null)
                {
                    HttpContext.Session.SetInt32(SessionKeyId, currentUser.UserID); //set the session var
                    staticUser = currentUser;
                    return RedirectToAction("Index", "User");
                }
            }
            return View(obj);
        }

        [HttpGet]
        public IActionResult Update()
        {
            var UserID = HttpContext.Session.GetInt32("_Id");
            if (UserID.HasValue)
            {
                return View(staticUser);
            }
            else
                return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult Update([Bind] UserInfo obj)
        {
            //if (ModelState.IsValid)
            {
                obj.UserID = staticUser.UserID;
                if (userDAL.UpdateUser(obj))
                {
                    staticUser = obj;
                    TempData["RegMessage"] = "Profile Update Successful!";
                    return RedirectToAction("Index", "User");
                }
                else return View();
            }
            //else return View(obj);

        }

    }
}
