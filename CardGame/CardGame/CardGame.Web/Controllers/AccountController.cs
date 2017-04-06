using System;
using System.Web;
using System.Web.Mvc;
using CardGame.Web.Models;
using CardGame.DAL.Logic;
using CardGame.DAL.Model;
using System.Web.Security;
using CardGame.Web.Models.UI;

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
                return View(login);
            }
            else
            {
                bool hasAccess = AuthManager.AuthUser(login.Email, login.Passwort);
                login.Role = UserManager.GetRoleNamesByUserEmail(login.Email);

                if (hasAccess)
                {
                    var authTicket = new FormsAuthenticationTicket(
                                    1,                            //ticketversion
                                    login.Email,                  //welcher User
                                    DateTime.Now,                 // Zeitpunkt der Erstellung
                                    DateTime.Now.AddMinutes(20),  // Gültigkeitsdauer
                                    true,                         // Persistentes Ticket über Sessions hinweg
                                    login.Role                   /* "admin"*/  //login.Role// Userrollen
                                    );

                    string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                    // optionale verschlüsselung
                    var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                    // neues Cookie erstellen, hat immer Name und Wert
                    System.Web.HttpContext.Current.Response.Cookies.Add(authCookie);
                    // Ticket schicken wir dem User
                }
                else
                {
                    return View(login);
                }
                return RedirectToAction("Index", "Home");
            }
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

        public ActionResult ErrorMessage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Register regUser)
        {
            if (ModelState.IsValid)
            {
                //    return View("Completed");
                //}
                //else
                //{
                //    ViewBag.Selpwd = regUser.Passwort;
                //    ViewBag.Selconfirmpwd = regUser.ConfirmPassword;

                //}
                var dbUser = new tblperson();
                if (regUser.Email != null)
                {
                    dbUser.firstname = regUser.Firstname;
                    dbUser.lastname = regUser.Lastname;
                    dbUser.email = regUser.Email;
                    dbUser.password = regUser.Passwort;
                    dbUser.gamertag = regUser.Gamertag;
                    dbUser.salt = regUser.Salt;
                    dbUser.userrole = "player";
                    dbUser.isactive = true;
                    dbUser.currencybalance = 1000;
                    AuthManager.Register(dbUser);
                    return RedirectToAction("Login");
                }
            }

            return View("Register");
        }


    }
}