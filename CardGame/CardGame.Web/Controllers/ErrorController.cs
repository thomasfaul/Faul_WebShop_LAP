using log4net;
using System.Web.Mvc;

namespace CardGame.Web.Controllers
{
    public class ErrorController : Controller
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #region Error
        /// <summary>
        /// Returns View
        /// </summary>
        /// <returns></returns>
        public ActionResult Error()
        {
            log.Info("ErrorController-Error");
            return View();
        } 
        #endregion
    }
}