using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using SmallRatings.Business;
using SmallRatings.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace SmallRatings.Controllers
{
    public class ProController : Controller
    {
        ProBusinessService proService = new ProBusinessService(); //use business service to pass and receive from data service
        public const string SessionUserId = "_Id";
        public const string SessionUserAvatar = "_UserAvatar";
        public const string SessionProId = "_Pro";
        public ActionResult Index()
        {
            var UserID = HttpContext.Session.GetInt32(SessionUserId);
            if (UserID.HasValue)
            {
                var ProID = HttpContext.Session.GetInt32(SessionProId);
                if (ProID.HasValue) //is logged in by checking session var.
                {
                    TempData["Registered"] = true;
                    return View();
                }
                else
                {
                    TempData["Registered"] = false;
                    return View();
                }
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RegisterBiz([Bind] ProInfo obj)
        {
            //Make sure the data is valid (another form of validation)
            if (ModelState.IsValid)
            {
                obj.UserID = (int) HttpContext.Session.GetInt32(SessionUserId);
                if (proService.CheckDupe(obj) == false) //cannot register duplicate usernames
                {
                    if (proService.NewBusiness(obj))
                    {
                        TempData["Registered"] = true;
                        return View("Index");
                    }
                    else TempData["Error"] = "Server Error, try again.";
                }
                else TempData["Error"] = "This business name already exists.";
            }
            TempData["Registered"] = false;
            return View("Index", obj);
        }

        // GET: ProfessionalsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProfessionalsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProfessionalsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProfessionalsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProfessionalsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProfessionalsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProfessionalsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
