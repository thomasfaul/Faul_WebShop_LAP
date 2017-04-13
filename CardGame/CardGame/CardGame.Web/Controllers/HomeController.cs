using System.Web.Mvc;
using CardGame.Web.Models;
using CardGame.DAL.Logic;
using CardGame.Web.HtmlHelpers;
using CardGame.Log;

namespace CardGame.Web.Controllers
{
    public class HomeController : Controller
    {
        #region ActionResult Index
        /// <summary>
        /// Creates the Index View
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Index()
        {
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
            var dbUser = UserManager.GetUserByUserEmail(User.Identity.Name);
            if (dbUser != null)
            {
                ViewBag.Id = dbUser.idperson;
                ViewBag.Firstname = dbUser.firstname;
                ViewBag.Lastname = dbUser.lastname;
                ViewBag.Email = dbUser.email;
                ViewBag.MyCurrency = dbUser.currencybalance;
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
                var dbUser = UserManager.GetUserByUserEmail(User.Identity.Name);
                if (dbUser != null)
                {
                    SessionHelper.Set<string>("Firstname", dbUser.firstname);
                    SessionHelper.Set<string>("Lastname", dbUser.lastname);
                    SessionHelper.Set<int>("Id", dbUser.idperson);
                    SessionHelper.Set<string>("Email", dbUser.password);
                    if (dbUser.currencybalance != null)
                    {
                        SessionHelper.Set<int>("CurrencyBalance", (int)dbUser.currencybalance);
                    }
                }
            }
            catch (System.Exception)
            {

                Writer.LogInfo("Problem in der Session");
            }
        } 
        #endregion
    }
}
