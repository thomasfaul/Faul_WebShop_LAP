﻿using CardGame.DAL.Logic;
using CardGame.Web.Models.Charts;
using System.Linq;
using System.Web.Helpers;
using System.Web.Mvc;

namespace CardGame.Web.Controllers.ChartController
{
    public class ChartController : Controller
    {

  
        public ActionResult ShowTopTenSellers()
        {
            ChartViewModel model = new ChartViewModel();
            ChartModel dbmodel= DBInfoManager.GetTop10Buyers();
            for (int i = 0; i < dbmodel.Info.Length; i++)
            {
                model.Infos[i] = dbmodel.Info;
                model.Werte[i] = dbmodel.Wert;
            }
            if (model.Infos==null||model.Werte==null)
            {

                TempData["ErrorMessage"] = "Aufruf des ShowTopTensellers nicht möglich";
                return null;
            }
            else
            {
            Chart bytes = new Chart(width: 300, height: 120)
            .AddSeries(
                chartType: "column",
                xValue: model.Infos,
                yValues: model.Werte)
            .AddSeries(
                chartType: "column",
                xValue: model.Infos,
                yValues: model.Werte);

            return File(bytes.GetBytes("png"), "image/png");
            }
        }



        public ActionResult ShowTopFiveClients()
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

    }
}