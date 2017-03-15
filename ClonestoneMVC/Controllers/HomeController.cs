using ClonestoneMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ClonestoneMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
                ViewBag.position = "Home";
                return View();
        }

        [HttpGet]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();
            return RedirectToAction("Index");
        }

    }
}