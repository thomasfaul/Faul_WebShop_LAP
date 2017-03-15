using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClonestoneMVC.Models;

namespace ClonestoneMVC.Controllers
{
    public class RankingController : Controller
    {
        private ClonestoneEntities db = new ClonestoneEntities();

        // GET: Ranking
        public ActionResult Index()
        {

            var ranking = db.pWinLoss();
            return View(ranking.ToList());
        }
    }
}