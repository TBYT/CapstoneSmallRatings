using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmallRatings.Business;
using SmallRatings.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmallRatings.Controllers
{
    public class ProController : Controller
    {
        ProBusinessService proService = new ProBusinessService(); //use business service to pass and receive from data service
        public const string SessionUserId = "_Id";
        public const string SessionUserAvatar = "_UserAvatar";
        public const string SessionProId = "_Pro";
        // GET: ProfessionalsController
        public ActionResult Index()
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RegisterBiz([Bind] ProInfo obj)
        {
            //Make sure the data is valid (another form of validation)
            if (ModelState.IsValid)
            {
                if (proService.CheckDupe(obj) == false) //cannot register duplicate usernames
                {
                    if (proService.NewBusiness(obj))
                    {
                        return View();
                    }
                }
                else TempData["UserExists"] = "This name already exists.";
            }
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
