using System.Web.Mvc;
using CardGame.Web.Models;
using CardGame.DAL.Logic;
using CardGame.Web.HtmlHelpers;
using System.Diagnostics;
using log4net;
using System;

namespace CardGame.Web.Controllers
{
    public class HomeController : Controller
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #region ActionResult Index
        /// <summary>
        /// Creates the Index View
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Index()
        {
            log.Info("HomeController-Index");
            if (User.Identity.Name != "")
            {
                CreateViewBag();
                CreateSession();
            }
            return View();
        }

        #endregion

        #region ActionResult About
        /// <summary>
        /// Creates the About View
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult About()
        {
            log.Info("HomeController-About");
            ViewBag.Message = "Your application description page.";
            CreateViewBag();
            return View();
        }

        #endregion

        #region ActionResult Contact
        /// <summary>
        /// Creates the Contact View
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Contact()
        {
            log.Info("HomeController-Contact");

            ViewBag.Message = "Your contact page.";
            CreateViewBag();
            return View();
        }
        #endregion

        #region ActionResult Statistics
        /// <summary>
        /// Creates the Statistics View
        /// </summary>
        /// <returns></returns>
        public ActionResult Statistics()
        {
            log.Info("HomeController-Statistics");
            Statistic s = new Statistic();

            //Befülle die Statistik
            s.NumUsers = DBInfoManager.GetNumUsers();
            s.NumCards = DBInfoManager.GetNumCards();
            s.NumDecks = DBInfoManager.GetNumDecks();
            CreateViewBag();


            return View(s);
        }
        #endregion

        #region Create ViewBag
        /// <summary>
        /// Creates a Viewbag, if you need the Information of the current User
        /// </summary>
        public void CreateViewBag()
        {
            log.Info("HomeController-CreateViewBag");
            var dbUser = UserManager.GetUserByUserEmail(User.Identity.Name);
            if (dbUser != null)
            {
                ViewBag.Id = dbUser.ID;
                ViewBag.Firstname = dbUser.FirstName;
                ViewBag.Lastname = dbUser.LastName;
                ViewBag.Email = dbUser.Email;
                ViewBag.MyCurrency = dbUser.AmountMoney;
            }
        }
        #endregion

        #region CREATE SESSION a funny Idea but...
        /// <summary>
        /// Creates my own Sessionvariables, but on the first load, they don´t work
        /// </summary>
        public void CreateSession()
        {
            try
            {
                log.Info("HomeController-CreateSession");
                var dbUser = UserManager.GetUserByUserEmail(User.Identity.Name);
                if (dbUser != null)
                {
                    SessionHelper.Set<string>("Firstname", dbUser.FirstName);
                    SessionHelper.Set<string>("Lastname", dbUser.LastName);
                    SessionHelper.Set<int>("Id", dbUser.ID);
                    SessionHelper.Set<string>("Email", dbUser.Password);
                    if (dbUser.AmountMoney != null)
                    {
                        SessionHelper.Set<int>("CurrencyBalance", (int)dbUser.AmountMoney);
                    }
                }
            }
            catch (Exception e)
            {
                Debugger.Break();
                log.Info("HomeController-CreateSession",e);
            }
        }
        #endregion


    }
}
