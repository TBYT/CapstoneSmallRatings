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
    public class UserController : Controller //
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
            var UserID = HttpContext.Session.GetInt32("_Id");
            if (UserID.HasValue == false)
                return View();
            else return View("Index");
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
                if (userDAL.CheckUserExists(obj.Username) == false)
                {
                    if (userDAL.AddUser(obj))
                    {
                        TempData["RegMessage"] = "Registration Successful. Navigate to Navbar to Login!";
                        return RedirectToAction("Index", "Home");
                    }
                }
                else TempData["UserExists"] = "This username already exists.";
            }
            return View(obj);
        }

        [HttpGet]
        public IActionResult Login()
        {
            var UserID = HttpContext.Session.GetInt32("_Id");
            if (UserID.HasValue == false)
                return View();
            else return View("Index");
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
        public IActionResult Update([Bind] UserInfo obj, IFormCollection form)
        {
            string NewPass = form["NewPass"].ToString();
            string ConfirmNewPass = form["ConfirmNewPass"].ToString();
            if (ModelState.IsValid)
            {
                if (obj.Username == staticUser.Username || userDAL.CheckUserExists(obj.Username) == false)
                {
                    if (NewPass != ConfirmNewPass)
                    {
                        TempData["NewPass"] = "New Passwords does not match each other";
                        return View(obj);
                    }
                    else if (obj.Password != staticUser.Password)
                    {
                        TempData["PasswordCheck"] = "Current Password does not match the one on record!";
                        return View(obj);
                    }
                    else
                    {
                        if (NewPass != "" && ConfirmNewPass != "")
                            obj.Password = NewPass;
                        obj.UserID = staticUser.UserID;
                        if (userDAL.UpdateUser(obj))
                        {
                            staticUser = obj;
                            TempData["RegMessage"] = "Profile Update Successful!";
                            return RedirectToAction("Index", "User");
                        }
                    }
                }
                else TempData["UserCheck"] = "This username is already taken.";
            }
            return View(obj);
        }

    }
}
