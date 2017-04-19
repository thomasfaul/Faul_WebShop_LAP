
using System.Web.Mvc;

namespace CardGame.Web.Controllers
{
    public class ErrorController : Controller
    {

        #region Error
        /// <summary>
        /// Returns View
        /// </summary>
        /// <returns></returns>
        public ActionResult Error()
        {
            return View();
        } 
        #endregion
    }
}