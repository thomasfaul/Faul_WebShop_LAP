using System.Collections.Generic;
using System.Web.Mvc;
using CardGame.DAL.Logic;
using CardGame.Web.Models.UI;
using log4net;

namespace CardGame.Web.Controllers
{
    public class UserController : Controller
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        #region ACTIONRESULT ADIM- USER 
        /// <summary>
        /// Returns the Usersdata for the Adminpage
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            log.Info("UserController-Index");

            List<Register> UserList = new List<Register>();
            var dbUserlist = UserManager.GetAllUser();

            foreach (var c in dbUserlist)
            {
                Register user = new Register();
                user.ID = c.ID;
                user.Firstname = c.FirstName;
                user.Lastname = c.LastName;
                user.Email = c.Email;
                user.Role = c.UserRole;
                user.Passwort = c.Password;
                user.Salt = c.Salt;
                if (c.AmountMoney != null)
                {
                    user.Currencybalance = (int)c.AmountMoney;
                }
                else
                {
                    user.Currencybalance = 0;
                }
                UserList.Add(user);

            }
            return View(UserList);
        }
        /// <summary>
        /// TODO set User inactive
        /// </summary>
        /// <returns></returns>
        public ActionResult UserInactive()
        {
            //var userinactive= UserManager.
            return View();
        }
        #endregion

    }
}