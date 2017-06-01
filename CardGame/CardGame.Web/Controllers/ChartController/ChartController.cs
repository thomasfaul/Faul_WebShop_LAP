using CardGame.DAL.Logic;
using CardGame.Web.Models.Charts;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Collections;
using log4net;

using CardGame.Web.Controllers.HtmlHelpers;

namespace CardGame.Web.Controllers.ChartController
{
    public class ChartController : Controller
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #region Show TOP 5 Buyers
        public ActionResult ShowTopFiveBuyers()
        {
            ChartViewModel model = new ChartViewModel();
            model.Infos = new ArrayList();
            model.Werte = new ArrayList();
            ChartModel dbmodel = DBInfoManager.GetTop10CustomersNames();

            model.Infos = dbmodel.Strings;
            model.Werte = dbmodel.Values;

            if (model.Infos == null || model.Werte == null)
            {

                TempData["ErrorMessage"] = "Aufruf des ShowTopFivesellers nicht möglich";
                return null;
            }
            else
            {
                Chart bytes = new Chart(width: 600, height: 220,theme: ChartThemes.GetMyCustomTheme())
                    .AddTitle("Die Top5 Kunden")
                    .AddSeries(
                    chartType: "column",
                    xValue: model.Infos,
                    yValues: model.Werte);


                return File(bytes.GetBytes("png"), "image/png");
            }
        }
        #endregion


        #region Show TOP Five Buyers/Email
        public ActionResult ShowTopFiveBuyersEmail()
        {
            ChartViewModel model = new ChartViewModel();
            model.Infos = new ArrayList();
            model.Werte = new ArrayList();
            ChartModel dbmodel = DBInfoManager.GetTop5CustomersEmail();

            model.Infos = dbmodel.Strings;
            model.Werte = dbmodel.Values;

            if (model.Infos == null || model.Werte == null)
            {

                TempData["ErrorMessage"] = "Aufruf des ShowTopTenBuyersEmail nicht möglich";
                return null;
            }
            else
            {
                Chart bytes = new Chart(width: 600, height: 220, theme: ChartThemes.GetMyCustomTheme())
                   .AddTitle("Die Top5 Kunden Emails")
                   .AddSeries(
                    chartType: "bar",
                    xValue: model.Infos,
                    yValues: model.Werte);


                return File(bytes.GetBytes("png"), "image/png");
            }
        }
        #endregion


        #region Show TOP Five Sellers
        public ActionResult ShowTop5Sellers()
        {
            ChartViewModel model = new ChartViewModel();
            model.Infos = new ArrayList();
            model.Werte = new ArrayList();
            ChartModel dbmodel = DBInfoManager.GetTop5Sellers();

            model.Infos = dbmodel.Strings;
            model.Werte = dbmodel.Values;

            if (model.Infos == null || model.Werte == null)
            {

                TempData["ErrorMessage"] = "Aufruf des ShowTop5Sellers nicht möglich";
                return null;
            }
            else
            {
                Chart bytes = new Chart(width: 600, height: 220,theme:ChartThemes.GetMyCustomTheme())
                    .AddTitle("Die Top5 Produkte")
                    .AddSeries(
                    chartType: "pie",
                    xValue: model.Infos,
                    yValues: model.Werte);


                return File(bytes.GetBytes("png"), "image/png");
            }
        }
        #endregion


    }
}