﻿https://www.youtube.com/watch?v=Q971J_7Znts
using System.Web.Mvc;
using CardGame.Web.Models;
using CardGame.DAL.Logic;

namespace CardGame.Web.Controllers
{
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            if (User.Identity.Name != "")
            {
                var dbUser = UserManager.GetUserByUserEmail(User.Identity.Name);
                ViewBag.Firstname = dbUser.firstname;
                ViewBag.Lastname = dbUser.lastname;
                ViewBag.Email = dbUser.email;
            }
            return View();
        }

        //[Authorize(Roles = "user,admin")]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Statistics()
        {
            Statistic s = new Statistic();

            //Befülle die Statistik
            s.NumUsers = DBInfoManager.GetNumUsers();
            s.NumCards = DBInfoManager.GetNumCards();
            s.NumDecks = DBInfoManager.GetNumDecks();

            return View(s);
        }


    }
}