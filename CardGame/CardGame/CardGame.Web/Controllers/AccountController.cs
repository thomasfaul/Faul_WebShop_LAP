using System;
using System.Web;
using System.Web.Mvc;
using CardGame.Web.Models;
using CardGame.DAL.Logic;
using CardGame.DAL.Model;
using System.Web.Security;
using CardGame.Web.Models.UI;
using CardGame.Web.HtmlHelpers;

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
        public ActionResult Login(Login login)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Bitte nochmal versuchen";
                return View(login);
            }
            else
            {
                bool hasAccess = AuthManager.AuthUser(login.Email, login.Passwort);
                
                if (!hasAccess)
                {

                    TempData["ErrorMessage"] = "Kein Zutritt";
                    return View(login);
                   
                }
            }

            string role = UserManager.GetRoleNamesByUserEmail(login.Email);
            auth(login.Email, login.Passwort, role);
            TempData["ConfirmMessage"] = "Sie sind eingeloggt";
            return RedirectToAction("Index", "Home");
        }

        private void auth(string email, string passwort, string role)
        {
            
                var authTicket = new FormsAuthenticationTicket(
                                1,                            //ticketversion
                                email,                  //welcher User
                                DateTime.Now,                 // Zeitpunkt der Erstellung
                                DateTime.Now.AddMinutes(20),  // Gültigkeitsdauer
                                true,                         // Persistentes Ticket über Sessions hinweg
                                role                  /* "admin"*/  //login.Role// Userrollen
                                );

                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                // optionale verschlüsselung
                var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                // neues Cookie erstellen, hat immer Name und Wert
                System.Web.HttpContext.Current.Response.Cookies.Add(authCookie);
                // Ticket schicken wir dem User
              
        }




        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            TempData["ConfirmMessage"] = "Sie sind ausgeloggt";
            return RedirectToAction("Index", "Home");
        }






        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }





        public ActionResult ErrorMessage()
        {
            return View();
        }





        [HttpPost]
        public ActionResult Register(Register regUser)
        {
            if (!ModelState.IsValid)
            {
                return View(regUser);

            }
            var dbUser = new tblperson();
            dbUser.firstname = regUser.Firstname;
            dbUser.lastname = regUser.Lastname;
            dbUser.email = regUser.Email;
            dbUser.password = regUser.Passwort;
            dbUser.gamertag = regUser.Gamertag;
            dbUser.salt = regUser.Salt;
            dbUser.userrole = "player";
            dbUser.isactive = true;
            dbUser.currencybalance = 1000;
            if (!AuthManager.Register(dbUser))
            {
                TempData["ErrorMessage"] = "Sie konnten nicht eingeloggt werden";
                return View(regUser);
            }

            auth(dbUser.email, dbUser.password, dbUser.userrole);
            TempData["ConfirmMessage"] = "Sie sind registriert";
            return RedirectToAction("Index", "Home");
        }
    }
}

