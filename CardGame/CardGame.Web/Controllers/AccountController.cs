using System;
using System.Web;
using System.Web.Mvc;
using CardGame.DAL.Logic;
using CardGame.DAL.Model;
using System.Web.Security;
using CardGame.Web.HtmlHelpers;
using log4net;
using CardGame.Web.Models.UI;


namespace CardGame.Web.Controllers
{
    public class AccountController : Controller
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #region ACTIONRESULT LOGIN
        /// <summary>
        /// Returns a View
        /// </summary>
        /// <returns>View</returns>
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login()
        {
            log.Info("AccountController-Login");
            return View();
        }
        #endregion

        #region ACTIONRESULT LOGIN II
        /// <summary>
        /// Takes the Login of a User,
        /// Checks if it is valid
        /// logs the user in
        /// creates the authentication Ticket
        /// authcookie wird erstellt
        /// </summary>
        /// <param name="login"></param>
        /// <returns>Actionresult</returns>
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(Login login)
        {
            log.Info("Accountcontroller-Login");
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Bitte nochmal versuchen";
                return View(login);
            }
            else
            {
                bool hasAccess = AuthManager.AuthUser(login.Email, login.Password);

                if (!hasAccess)
                {

                    TempData["ErrorMessage"] = "Kein Zutritt";
                    return View(login);

                }
            }
            bool isdeleted = UserManager.GetIfUserIsDeleted(login.Email);
            if (isdeleted == true)
            {

                TempData["ErrorMessage"] = "Sie haben ihren Account  vor geraumer Zeit gelöscht";
                return View(login);

            }
            bool isnotbaned = UserManager.GetIfUserIsBanned(login.Email);

            if (isnotbaned==true)
            {

                TempData["ErrorMessage"] = "Sie sind für drei Tage gesperrt";
                return View(login);

            }
            string role = UserManager.GetRoleNamesByUserEmail(login.Email);
            
            auth(login.Email, login.Password, role);
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
        #endregion

        #region ACTIONRESULT REGISTER
        /// <summary>
        /// Returns View
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Register()
        {
            log.Info("Accountcontroller-Register");
            return View();
        }
        #endregion

        #region ACTIONRESULT REGISTER II
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Register(Register regUser)
        {
            log.Info("Accountcontroller-RegisterII");

            if (!ModelState.IsValid)
            {
                return View(regUser);

            }
            var dbUser = new User();
            dbUser.FirstName = regUser.Firstname;
            dbUser.LastName = regUser.Lastname;
            dbUser.Email = regUser.Email;
            dbUser.Password = regUser.Password;
            dbUser.GamerTag = regUser.Gamertag;
            dbUser.Salt = regUser.Salt;
            dbUser.UserRole = "player";
            dbUser.IsActive = true;
            dbUser.AmountMoney = 100;
            dbUser.EntryDate = DateTime.Now;
            dbUser.IsDeleted = false;
            dbUser.Zip = regUser.Zip;
            dbUser.City = regUser.City;
            dbUser.Address = regUser.Address;
            


            //bool saved = UserManager.SaveUsersAdress(User.Identity.Name,regUser.Address,regUser.Zip,regUser.City );
            bool ok= AuthManager.Register(dbUser);
            //if (saved != true)
            //{
            //    TempData["ErrorMessage"] = "Eintragen der Addressdaten war nicht möglich";
            //    return View(regUser);
            //}
            if (ok != true)
            {
                TempData["ErrorMessage"] = "Sie konnten nicht registriert werden";
                return View(regUser);
            }
            else
            {
             auth(dbUser.Email, dbUser.Password, dbUser.UserRole);
             int userID = UserManager.Get_UserByEmail(dbUser.Email).ID;
             DeckManager.AddDefaultDecksByUserId(userID);
             TempData["ConfirmMessage"] = "Sie sind registriert";
              bool emailworked = EmailHelper.SendEmail(dbUser.Email, "Registrierungsbestätigung", "Lieber User, Sie haben sich erfolgreich im Cloneshop registriert");
              return RedirectToAction("Index", "Home");
            }
            
        } 
        #endregion

        #region ACTIONRESULT LOGOUT
        /// <summary>
        /// Creates the Sign out
        /// </summary>
        /// <returns>RedirectToAction</returns>
        public ActionResult Logout()
        {
            log.Info("Accountcontroller-Logout");
            FormsAuthentication.SignOut();
            TempData["ConfirmMessage"] = "Sie sind ausgeloggt";
            return RedirectToAction("Index", "Home");
        }
        #endregion

        #region ACTIONRESULT ERRORMESSAGE
        /// <summary>
        /// Returns View and shows the ErrorMessage
        /// "muss sich ein Fehler eingeschlichen haben"
        /// </summary>
        /// <param name="regUser"></param>
        /// <returns>View</returns>
        public ActionResult ErrorMessage()
        {
            log.Info("Accountcontroller-ErrorMessage");
            TempData["ErrorMessage"] = "muss sich ein Fehler eingeschlichen haben";
            return View();

        }
        #endregion


        #region ACTIONRESULT PASSWORDRESET
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Passwordreset()
        {
            return View();
        }
        #endregion

        #region ACTIONRESULT PASSWORDRESETII
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Passwordreset(string Email)
        {
            var user = UserManager.Get_UserByEmail(Email);

            bool ok = EmailHelper.SendPasswordResetEmail(user.Email, user.ID, user.FirstName);
            TempData["ConfirmMessage"] = "Du Hast eine Email bekommen, bitte Postfach überprüfen";
            return RedirectToAction("Login");
        }
        #endregion

        #region ACTIONRESULT RESETPASSWORD
        [AllowAnonymous]
        public ActionResult ResetPassword(int id)
        {
            var user = UserManager.Get_UserById(id);
            string salt = Helper.GenerateSalt();
            string hashedAndSaltedPassword = Helper.GenerateHash("123user!" + salt);
            bool ok = AuthManager.ResetThePassword(hashedAndSaltedPassword, salt, User.Identity.Name);
            if (ok == true)
            {
                EmailHelper.SendPasswordResetEmailAnswer(User.Identity.Name, id);
            }
            return RedirectToAction("Login");
        } 
        #endregion

    }

}


