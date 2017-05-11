using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CardGame.Web.Controllers.ChartController
{
    public class ChartController : Controller
    {



        public PartialViewResult _TopFiveSellers()
        {
            return PartialView();
        }



        public PartialViewResult _TopFiveClients()
        {
            return PartialView();
        }



        public PartialViewResult _SellingStatsWeek()
        {
            return PartialView();
        }



        public PartialViewResult _SellingStatsMonth()
        {
            return PartialView();
        }

    }
}