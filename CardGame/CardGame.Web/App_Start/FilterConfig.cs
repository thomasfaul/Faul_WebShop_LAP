using System.Web;
using System.Web.Mvc;

namespace CardGame.Web
{
    public class FilterConfig
    {
        #region Register Global Filters
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
        #endregion
    }
}
