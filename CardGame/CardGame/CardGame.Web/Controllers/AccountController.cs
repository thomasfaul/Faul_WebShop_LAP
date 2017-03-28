using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CardGame.Web.Models;
using CardGame.DAL.Logic;
using CardGame.DAL.Model;
using CardGame.Log;
using System.Web.Security;

namespace CardGame.Web.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User login)
        {
            bool hasAccess = AuthManager.AuthUser(login.Email, login.Password);
            login.Role = UserManager.GetRoleNamesByUserName(login.Email);

            if (hasAccess)
            {
                // 
                var authTicket = new FormsAuthenticationTicket(
                                1,                            //ticketversion
                                login.Email,                  //welcher User
                                DateTime.Now,                 // Zeitpunkt der Erstellung
                                DateTime.Now.AddMinutes(20),  // Gültigkeitsdauer
                                true,                         // Persistentes Ticket über Sessio9ns hinweg
                               login.Role                   /* "admin"*/  //login.Role// Userrollen
                                );

                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                // optionale verschlüsselung
                var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                // neues Cookie erstellen, hat immer Name und Wert
                System.Web.HttpContext.Current.Response.Cookies.Add(authCookie);
                // Ticket schicken wir dem User
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User regUser)
        {
            var dbUser = new tblperson();

            dbUser.firstname = regUser.Firstname;
            dbUser.lastname = regUser.Lastname;

            dbUser.email = regUser.Email;
            dbUser.password = regUser.Password;
            dbUser.salt = regUser.Salt;
            dbUser.userrole = "player";
            dbUser.isactive = true;
            dbUser.currencybalance = 1000;

            //dbUser.tblrole = new List<tblrole>();
            //dbUser.tblrole.Add(new tblrole());
            //dbUser.tblrole.FirstOrDefault().rolename = "user";

            AuthManager.Register(dbUser);

            return RedirectToAction("Login");
        }
    }
}