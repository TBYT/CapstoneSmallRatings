using Microsoft.AspNetCore.Mvc;
using System;
using SmallRatings.Business;
using SmallRatings.Models;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace SmallRatings.Controllers
{
    public class UserController : Controller //
    {
        UserBusinessService userDAL = new UserBusinessService();
        public const string SessionKeyId = "_Id";
        static UserInfo staticUser;

        [HttpGet]
        public IActionResult Index()
        { //soon to be social feed to display businesses.
            
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
        [ValidateAntiForgeryToken]
        public IActionResult Register([Bind]UserInfo obj)
        {
            //Make sure the data is valid (another form of validation
            if(ModelState.IsValid)
            {
                if (userDAL.CheckUserExists(obj.Username) == false)
                {
                    if (userDAL.AddUser(obj))
                    {
                        TempData["RegMessage"] = "Registration Successful. You may now login!";
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
        [ValidateAntiForgeryToken]
        public IActionResult Login([Bind] LoginInfo obj)
        {
            //Make sure the data is valid (another form of validation
            if (ModelState.IsValid)
            {
                UserInfo currentUser = userDAL.LoginUser(obj);
                if (currentUser!=null)
                {
                    HttpContext.Session.SetInt32(SessionKeyId, currentUser.UserID); //set the session var
                    if(currentUser.Avatar!=null) 
                        HttpContext.Session.SetString("_UserAvatar", "data:"+currentUser.AvatarFileType+";base64,"+Convert.ToBase64String(currentUser.Avatar)); 
                    staticUser = currentUser;
                    return RedirectToAction("Index", "User");
                }
            }
            return View(obj);
        }

        [HttpGet]
        public IActionResult Settings()
        {
                var UserID = HttpContext.Session.GetInt32("_Id");
                if (UserID.HasValue)
                {
                    //return user information to the view if logged in from session.
                    return View(staticUser);
                }
                else
                    return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Settings([Bind] UserInfo obj, IFormCollection form)
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
                        obj.UserID = staticUser.UserID; //no user id was passed in form, it will be passed here.
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
                            HttpContext.Session.SetString("_UserAvatar", "data:"+obj.AvatarFileType+";base64,"+Convert.ToBase64String(obj.Avatar));
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
