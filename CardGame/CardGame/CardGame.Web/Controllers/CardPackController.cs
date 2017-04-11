using CardGame.DAL.Logic;
using CardGame.DAL.Model;
using CardGame.Web.Models;
using CardGame.Web.HtmlHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CardGame.Web.Controllers
{
    public class CardPackController : Controller
    {
        HomeController a = new HomeController();
        public int PPagesize = 20;
        // GET: Cardpack
        public ActionResult PackOverview(int page = 1)
        {
            PacksListViewModel model = new PacksListViewModel();
            
            model.PIsMoney = false;
            try
            {
                model.PIsMoney = (bool)this.Session["isCurrency"];
            }
            catch (Exception){}
            
            List<CardPack> PackList = new List<CardPack>();
            var dbPacklist = PackManager.GetAllPacks();

            foreach (var p in dbPacklist)
            {
                CardPack pack = new CardPack();
                pack.IdPack = p.idpack;
                pack.PackName = p.packname;
                pack.IsMoney = p.ismoney;
                pack.PackPrice= (decimal)p.packprice;
                pack.Flavor = p.flavour;
                pack.Pic = p.packimage;
                PackList.Add(pack);
            }
            
            model.Packs = PackList.OrderBy(c => c.IdPack)
                           .Where(p => p.IsMoney == model.PIsMoney)
                           .Skip((page - 1) * PPagesize)
                           .Take(PPagesize).ToList<CardPack>();

            model.PagingInfo = new PageInfo
            {
                CurrentPage = page,
                ItemsPerPage = PPagesize,
                TotalItems = PackList.Count()
            };
           
            return View(model);
        }

        public ActionResult EnableCurrency() {
            this.Session["isCurrency"] = true;
            TempData["Infomessage"] = "LOADING";
            return RedirectToAction("PackOverview", "CardPack" );
           
        }

        public ActionResult DisableCurrency()
        {
            this.Session["isCurrency"] = false;
            TempData["Infomessage"] = "Packs are LOADING";
            //return RedirectToAction("PackOverview", "CardPack");
            return RedirectToAction("PackOverview");
        }

        //public ActionResult Details(int id)
        //{
        //    tblpack dbpack = null;

        //    dbpack = PackManager.GetPackById(id);

        //    CardPack pack = new CardPack();
        //    pack.IdPack = dbpack.idpack;
        //    pack.PackName = dbpack.packname;
        //    pack.PackPrice = (decimal)dbpack.packprice;
        //    pack.Pic = dbpack.packimage;
        //    pack.Flavor = dbpack.flavour;
        //    return View(pack);
        //}
    }
}