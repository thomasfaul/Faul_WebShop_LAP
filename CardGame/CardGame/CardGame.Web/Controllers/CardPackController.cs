using CardGame.DAL.Logic;
using CardGame.Log;
using CardGame.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace CardGame.Web.Controllers
{
    public class CardPackController : Controller
    {
        HomeController a = new HomeController();
        public int PPagesize = 20;

        #region ACTIONRESULT PACKOVERVIEW
        /// <summary>
        /// checks if its a Currencyview
        /// takes the Pagesize
        /// and gets the Cardpacks
        /// returns the model
        /// </summary>
        /// <param name="page"></param>
        /// <returns>ActionResult</returns>
        public ActionResult PackOverview(int page = 1)
        {
            PacksListViewModel model = new PacksListViewModel();
            model.PIsMoney = false;
            try
            {
                model.PIsMoney = (bool)Session["isCurrency"];
            }
            catch (Exception e)
            {
                Writer.LogError(e);
            }

            List<CardPack> PackList = new List<CardPack>();
            var dbPacklist = PackManager.GetAllPacks();

            foreach (var p in dbPacklist)
            {
                CardPack pack = new CardPack();
                pack.IdPack = p.idpack;
                pack.PackName = p.packname;
                pack.IsMoney = p.ismoney ?? model.PIsMoney;
                pack.PackPrice = (decimal)p.packprice;
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
        #endregion

        #region ACTIONRESULT ENABLE CURRENCY
        /// <summary>
        /// ENABLES THE ISCURRENCYPAGE (PAY IN REAL)
        /// </summary>
        /// <returns></returns>
        public ActionResult EnableCurrency()
        {
            this.Session["isCurrency"] = true;
            TempData["Infomessage"] = "LOADING";
            return RedirectToAction("PackOverview", "CardPack");

        }
        #endregion

        #region ACTIONRESULT DISABLE CURRENCY
        /// <summary>
        /// ENABLES THE ISCURRENCYPAGE (PAY IN INGAME MONEY)
        /// </summary>
        /// <returns></returns>
        public ActionResult DisableCurrency()
        {
            this.Session["isCurrency"] = false;
            TempData["Infomessage"] = "Packs are LOADING";
            //return RedirectToAction("PackOverview", "CardPack");
            return RedirectToAction("PackOverview");
        }
        #endregion

        #region ACTIONRESULT BUY CARD PACK
        /// <summary>
        /// Takes the id of the cardpack and
        /// returns the View
        /// </summary> [Authorize(Roles = "player")]
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult BuyCardPack(int packid)
        {
            var dbCardPack = ShopManager.Get_CardPackById(packid);

            CardPack cardPack = new CardPack();
            cardPack.IdPack = dbCardPack.idpack;
            cardPack.PackName = dbCardPack.packname;
            cardPack.IsMoney = (bool)dbCardPack.ismoney;
            cardPack.NumCards = dbCardPack.numcards ?? 0;
            cardPack.PackPrice = dbCardPack.packprice ?? 0;
            cardPack.Flavor = dbCardPack.flavour;
            

            return View(cardPack);
        }
        #endregion

        #region ACTIONRESULT DETAILS: TODO

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
        #endregion
    }
}