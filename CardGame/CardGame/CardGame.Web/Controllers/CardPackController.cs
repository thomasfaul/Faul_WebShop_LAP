﻿using CardGame.DAL.Logic;
using CardGame.DAL.Model;
using CardGame.Log;
using CardGame.Web.Models;
using CardGame.Web.Models.DB;
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
                pack.Worth = p.worth ?? 0;
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

        #region ACTIONRESULT ORDEROVERVIEW
        /// <summary>
        /// Return the Overview of the Orders and the Order
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult OrderOverview()
        {
            Order o = (Order)TempData["Order"];
            TempData["Order"] = o;
            return View(o);
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
        public ActionResult BuyCardPack(int id)
        {
            var dbCardPack = ShopManager.Get_CardPackById(id);

            CardPack cardPack = new CardPack();
            cardPack.IdPack = dbCardPack.idpack;
            cardPack.PackName = dbCardPack.packname;
            cardPack.Worth = dbCardPack.worth ?? 0;
            cardPack.IsMoney = (bool)dbCardPack.ismoney;
            cardPack.NumCards = dbCardPack.numcards ?? 0;
            cardPack.PackPrice = dbCardPack.packprice ?? 0;
            cardPack.Flavor = dbCardPack.flavour;
       
            return View(cardPack);
        }
        #endregion

        #region ACTIONRESULT BUY CARD PACK
        /// <summary>
        /// Takes the ID and the Number of the Packs
        /// Saves the Packs in the Databace
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="numPacks"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult BuyCardPack(int id, int numPacks)
        {
            Writer.LogInfo("id: " + id.ToString());
            Writer.LogInfo("numPacks: " + numPacks.ToString());

            Order o = new Order();
            var dbCardPack = ShopManager.Get_CardPackById(id);

            CardPack cardPack = new CardPack();
            cardPack.IdPack = dbCardPack.idpack;
            cardPack.PackName = dbCardPack.packname;
            cardPack.NumCards = dbCardPack.numcards ?? 0;
            cardPack.PackPrice = dbCardPack.packprice ?? 0;
            cardPack.IsMoney = dbCardPack.ismoney ?? false;

            o.Pack = cardPack;
            o.Quantity = numPacks;
            o.CurrencyBalance = UserManager.Get_BalanceByEmail(User.Identity.Name);

            TempData["Order"] = o;

            return RedirectToAction("OrderOverview");
        }
        #endregion

        #region ACTIONRESULT SHOW GENERATED CARDS
        /// <summary>
        /// Gets the Generated Cards and returns a List
        /// of Cards to the View
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ShowGeneratedCards()
        {
            var orderedCards = (List<tblcard>)TempData["OrderedCards"];
            var cards = new List<Card>();

            foreach (var c in orderedCards)
            {
                Card card = new Card();
                card.ID = c.idcard;
                card.Name = c.cardname;
                card.Type = c.tbltype.typename;
                card.Mana = c.mana;
                card.Attack = c.attack;
                card.Life = c.life;
                card.Pic = c.pic;
                cards.Add(card);
            }
            return View(cards);
        }
        #endregion

        #region ACTIONRESULT NOT ENOUGH BALANCE
        /// <summary>
        /// Returns the "Not Enough Balance" View
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult NotEnoughBalance()
        {
            return View();
        }  
        #endregion
    }
}