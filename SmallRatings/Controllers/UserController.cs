using Microsoft.AspNetCore.Mvc;
using System;
using SmallRatings.Business;
using SmallRatings.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Collections.Generic;

namespace SmallRatings.Controllers
{
    public class UserController : Controller //
    {
        UserBusinessService userDAL = new UserBusinessService(); //use business service to pass and receive from data service
        ProBusinessService proService = new ProBusinessService(); //used one time in this class, to set session key if user does have business.
        public const string SessionUserId = "_Id";
        public const string SessionUserAvatar = "_UserAvatar";
        public const string SessionProId = "_Pro";
        static UserInfo staticUser;

        /*
         * The social feed for users that will be home to display businesses.
         */
        [HttpGet]
        public IActionResult Index()
        { //soon to be social feed to display businesses.
            
                var UserID = HttpContext.Session.GetInt32(SessionUserId);
                if (UserID.HasValue) //is logged in by checking session var.
                {
                    ProInfo[] Businesses = (ProInfo[])proService.GetAllBiz();
                    return View(Businesses);
                }
                else
                    return RedirectToAction("Login", "User");
        }

        [HttpGet]
        public IActionResult Register()
        {
                var UserID = HttpContext.Session.GetInt32(SessionUserId);
                if (UserID.HasValue == false) //is not registered by checking session var.
                return View();
                else return View("Index");
            }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register([Bind]UserInfo obj)
        {
            //Make sure the data is valid (another form of validation
            if(ModelState.IsValid)
            {
                if (userDAL.CheckUserExists(obj.Username) == false) //cannot register duplicate usernames
                {
                    if (userDAL.AddUser(obj))
                    {
                        //TempData["RegMessage"] = "Registration Successful. You may now login!";
                        return RedirectToAction("Index", "Home");
                    }
                    else TempData["Error"] = "Server Error. Please try again.";
                }
                else TempData["Error"] = "This username already exists.";
            }
            return View(obj);
        }

        [HttpGet]
        public IActionResult Login()
        {
                var UserID = HttpContext.Session.GetInt32(SessionUserId);
                if (UserID.HasValue == false) //is not logged in by checking session var.
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
        [ValidateAntiForgeryToken]
        public IActionResult Login([Bind] LoginInfo obj)
        {
            //Make sure the data is valid (another form of validation
            if (ModelState.IsValid)
            {
                UserInfo currentUser = userDAL.LoginUser(obj);
                if (currentUser!=null)
                {
                    int ProID = proService.GetPro(currentUser.UserID);
                    if (ProID!=-1)
                    {
                        HttpContext.Session.SetInt32(SessionProId, ProID);
                    }
                    HttpContext.Session.SetInt32(SessionUserId, currentUser.UserID); //set the session var
                    if(currentUser.Avatar!=null) 
                        HttpContext.Session.SetString(SessionUserAvatar, "data:"+currentUser.AvatarFileType+";base64,"+Convert.ToBase64String(currentUser.Avatar)); 
                    staticUser = currentUser;
                    return RedirectToAction("Index", "User");
                }
                TempData["Error"] = "Username or Password entered is incorrect.";
            }
            return View(obj);
        }

        [HttpGet]
        public IActionResult Settings()
        {
                var UserID = HttpContext.Session.GetInt32(SessionUserId);
                if (UserID.HasValue)
                {
                    //return user information to the view if logged in from session.
                    return View(staticUser);
                }
                else
                    return RedirectToAction("Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Settings([Bind] UserInfo obj, IFormCollection form)
        {
            string NewPass = form["NewPass"].ToString();
            string ConfirmNewPass = form["ConfirmNewPass"].ToString();
            if (ModelState.IsValid)
            {
                //if username is the same, or if changed, new username is not duplicate.
                if (obj.Username == staticUser.Username || userDAL.CheckUserExists(obj.Username) == false)
                {
                    //if user desired to change passwords
                    if (NewPass != ConfirmNewPass)
                    {
                        TempData["NewPass"] = "New Passwords does not match each other";
                        return View(obj);
                    }
                    else if (obj.Password != staticUser.Password) //must confirm current password to what we have stored.
                    {
                        TempData["PasswordCheck"] = "Current Password does not match the one on record!";
                        return View(obj);
                    }
                    else
                    {
                        if (NewPass != "" && ConfirmNewPass != "") //if user desired to change pass and it is not blank, update password. 
                            obj.Password = NewPass;
                        obj.UserID = staticUser.UserID; //no user id was passed in form, it will be passed here like how we passed the values to the form.
                        // line 141/142 so that there are no null values as img properties was not passed to form, relies on if user inputs.
                        obj.AvatarFileType = staticUser.AvatarFileType;
                        obj.Avatar = staticUser.Avatar;
                        if (form.Files.Count != 0)
                        {
                            using (var memoryStream = new MemoryStream())
                            {
                                form.Files[0].CopyTo(memoryStream); //copy image file data to memory stream.
                                byte[] imageBytes = memoryStream.ToArray(); //convert memory stream to bytes to store in database.
                                obj.Avatar = imageBytes;
                                obj.AvatarFileType = form.Files[0].ContentType; //need file type to display image for user.
                            }
                        }
                        if (userDAL.UpdateUser(obj))
                        {
                            staticUser = obj;
                            if(obj.Avatar!=null)
                                HttpContext.Session.SetString(SessionUserAvatar, "data:"+obj.AvatarFileType+";base64,"+Convert.ToBase64String(obj.Avatar));
                            //TempData["RegMessage"] = "Profile Update Successful!";
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
