using CardGame.DAL.Logic;
using CardGame.Web.Models.Charts;
using System.Linq;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Collections;

namespace CardGame.Web.Controllers.ChartController
{
    public class ChartController : Controller
    {


        #region Show TOP Ten Buyers
        public ActionResult ShowTopTenBuyers()
        {
            ChartViewModel model = new ChartViewModel();
            model.Infos = new ArrayList();
            model.Werte = new ArrayList();
            ChartModel dbmodel = DBInfoManager.GetTop10Buyers();

            model.Infos = dbmodel.Strings;
            model.Werte = dbmodel.Values;

            if (model.Infos == null || model.Werte == null)
            {

                TempData["ErrorMessage"] = "Aufruf des ShowTopTensellers nicht möglich";
                return null;
            }
            else
            {
                Chart bytes = new Chart(width: 600, height: 220)
                .AddSeries(
                    chartType: "column",
                    xValue: model.Infos,
                    yValues: model.Werte);


                return File(bytes.GetBytes("png"), "image/png");
            }
        }
        #endregion

        #region SHOW SIGN UPS LAST WEEK
        public ActionResult ShowSignUpsLastWeek()
        {
            ChartViewModel model = new ChartViewModel();
            model.Infos = new ArrayList();
            model.Werte = new ArrayList();
            ChartModel dbmodel = DBInfoManager.GetUserSignUpsWeekEmail();

            model.Infos = dbmodel.Strings;
            model.Werte = dbmodel.Values;

            if (model.Infos == null || model.Werte == null)
            {

                TempData["ErrorMessage"] = "Aufruf des ShowTopTensellers nicht möglich";
                return null;
            }
            else
            {
                Chart bytes = new Chart(width: 600, height: 220)
                .AddSeries(
                    chartType: "column",
                    xValue: model.Infos,
                    yValues: model.Werte);


                return File(bytes.GetBytes("png"), "image/png");
            }
        }
        #endregion

        #region Show PURCHASE ALL DAY
        public ActionResult ShowPurchaseAllDay()
        {
            ChartViewModel model = new ChartViewModel();
            model.Infos = new ArrayList();
            model.Werte = new ArrayList();
            ChartModel dbmodel = DBInfoManager.GetPurchasesCountWeek();

            model.Infos = dbmodel.Strings;
            model.Werte = dbmodel.Values;

            ChartViewModel modell = new ChartViewModel();
            modell.Infos = new ArrayList();
            modell.Werte = new ArrayList();
            ChartModel dbmodell = DBInfoManager.GetPurchasesSumWeek();

            model.Infos = dbmodel.Strings;
            model.Werte = dbmodel.Values;
            if (model.Infos == null || model.Werte == null)
            {

                TempData["ErrorMessage"] = "Aufruf des ShowPurchaseAllDay nicht möglich";
                return null;
            }
            else
            {
                Chart chart = new Chart(width: 600, height: 220)
              .AddSeries(
                  chartType: "column",
                  xValue: model.Infos,
                  name: "Gezählte Verkäufe",
                  yValues: model.Werte)
                .AddSeries(
                    chartType: "column",
                    xValue: modell.Werte,
                    name: "Summe Verkäufe",
                    yValues: modell.Infos);

                return File(chart.GetBytes("png"), "image/png");
            }

        }
        #endregion

        #region SHOW SELLING STATS LAST 24 Hours Sum 
        public ActionResult ShowSellingStatsLast24HoursSum()
        {
            ChartViewModel model = new ChartViewModel();
            model.Infos = new ArrayList();
            model.Werte = new ArrayList();
            ChartModel dbmodel = DBInfoManager.GetSellingStatsLast21HoursSum();
            model.Infos = dbmodel.Strings;
            model.Werte = dbmodel.Values;

            if (model.Infos == null || model.Werte == null)
            {

                TempData["ErrorMessage"] = "Aufruf des ShowTopTensellers nicht möglich";
                return null;
            }
            else
            {
                Chart bytes = new Chart(width: 600, height: 220)
                .AddSeries(
                    chartType: "column",
                    xValue: model.Infos,
                    yValues: model.Werte);


                return File(bytes.GetBytes("png"), "image/png");
            }
        }
        #endregion

        #region SHOW SIGN UPS LAST WEEK
        public ActionResult ShowPurchaseCountWeek()
        {
            ChartViewModel model = new ChartViewModel();
            model.Infos = new ArrayList();
            model.Werte = new ArrayList();
            ChartModel dbmodel = DBInfoManager.GetPurchasesCountWeek();

            model.Infos = dbmodel.Strings;
            model.Werte = dbmodel.Values;

            if (model.Infos == null || model.Werte == null)
            {

                TempData["ErrorMessage"] = "Aufruf des ShowTopTensellers nicht möglich";
                return null;
            }
            else
            {
                Chart bytes = new Chart(width: 600, height: 220)
                .AddSeries(
                    chartType: "column",
                    xValue: model.Infos,
                    yValues: model.Werte);


                return File(bytes.GetBytes("png"), "image/png");
            }
        }
        #endregion

        #region AT WORK
        public ActionResult ShowSellingStatsWeek()
        {
            //string theme = @"<Chart BackColor=""WhiteSmoke""><ChartAreas><ChartArea Name=""Default"" BackColor=""White""></ChartAreas><Legends><Legend _Template_=""All"" BackColor=""Transparent"" Font=""Trebuchet MS, 9pt"" IsTextAutoFit=""False"" Docking=""Bottom"" /></Legends></Chart>";

            Chart chart = new Chart(width: 300, height: 120 /*theme: theme*/)
                .AddSeries(
                    chartType: "column",
                    xValue: new[] { "Wert1", "Wert2", "Wert3", "Wert4" },
                    name: "Serie 1",
                    yValues: new[] { "12", "3", "23", "11" })
                .AddSeries(
                    chartType: "column",
                    xValue: new[] { "Wert1", "Wert2", "Wert3", "Wert4" },
                    name: "Serie 2",
                    yValues: new[] { "13", "32", "23", "5" });

            chart.AddLegend();

            return File(chart.GetBytes("png"), "image/png");
        }



        public ActionResult ShowSellingStatsMonth()
        {
            Chart chart = new Chart(width: 300, height: 120)
              .AddSeries(
                  chartType: "column",
                  xValue: new[] { "Wert1", "Wert2", "Wert3", "Wert4" },
                  name: "Serie 1",
                  yValues: new[] { "12", "3", "23", "11" })
              .AddSeries(
                  chartType: "column",
                  xValue: new[] { "Wert1", "Wert2", "Wert3", "Wert4" },
                  name: "Serie 2",
                  yValues: new[] { "13", "32", "23", "5" });

            chart.AddLegend();

            return File(chart.GetBytes("png"), "image/png");
        } 
        #endregion

    }
}