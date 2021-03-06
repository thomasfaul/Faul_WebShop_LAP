﻿using CardGame.DAL.Logic;
using CardGame.DAL.Model;
using CardGame.Web.HtmlHelpers;
using CardGame.Web.Models;
using CardGame.Web.Models.DB;
using CardGame.Web.Models.UI;
using log4net;
using Rotativa;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;



namespace CardGame.Web.Controllers
{
    public class CardPackController : Controller
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        HomeController a = new HomeController();
        public int PPagesize = 6;


        #region ACTIONRESULT DISCOUNTOVERVIEW
        /// <summary>
        /// checks if its a Currencyview
        /// takes the Pagesize
        /// and gets the Cardpacks
        /// returns the model
        /// </summary>
        /// <param name="page"></param>
        /// <returns>ActionResult</returns>
        public ActionResult DiscountOverview(int page = 1)
        {
            log.Info("CardPackController-PackOverview");
            DiscountListViewModel model = new DiscountListViewModel();
            model.PIsMoney = false;
            try
            {
                model.PIsMoney = (bool)Session["isCurrency"];
            }
            catch (Exception e)
            {
                //Debugger.Break();
                log.Error("CardPackController-Discount", e);
            }

            List<Web.Models.Discount> DiscList = new List<Web.Models.Discount>();
            var dbDiscountlist = PackManager.GetAlltDiscounts();
            var dbPacklist = PackManager.GetAllPacks();
            foreach (var d in dbDiscountlist)
            {
                Web.Models.Discount disc = new Web.Models.Discount();
                disc.ID = d.ID;
                disc.EndDate = d.EndDate?? DateTime.MinValue;
                disc.StartDate= d.StartDate ?? DateTime.MinValue;
                disc.DiscountAmount = d.Discount ??0;
                CardPack p = new CardPack();
                p.PackName = d.Pack.Name;
                p.PackPrice =(int) d.Pack.Price;
                p.Pic = d.Pack.Image;
                p.IdPack = d.Pack.ID;
                p.IsActive = d.Pack.IsActive ??false;
                p.PackPrice = d.Pack.Price ?? 0;
                p.IsMoney = d.Pack.IsMoney??false;
                
                

                disc.DiscountPack = p;
                DiscList.Add(disc);
            }
            
            DiscList = DiscList.Where(d => d.DiscountAmount != 0).ToList();
            DiscList = DiscList.Where(d => d.EndDate >DateTime.Now).ToList();
            DiscList = DiscList.Where(d => d.DiscountPack.IsActive).OrderBy(d=>d.DiscountPack.IsMoney).ToList();
            
            model.Discounts = DiscList;
            model.PagingInfo = new PageInfo
            {
                CurrentPage = page,
                ItemsPerPage = PPagesize,
                TotalItems = DiscList.Count()
            };

            return View(model);
        }
        #endregion

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
            log.Info("CardPackController-PackOverview");
            PacksListViewModel model = new PacksListViewModel();
            model.PIsMoney = false;
            try
            {
                model.PIsMoney = (bool)Session["isCurrency"];
               
            }
            catch (Exception e)
            {
                //Debugger.Break();
                log.Error("CardPackController-PackOverview",e);
            }

            List<CardPack> PackList = new List<CardPack>();
            var dbPacklist = PackManager.GetAllPacks();
            int us = UserManager.Get_UserByEmail(User.Identity.Name).AmountMoney ??0;
            foreach (var p in dbPacklist)
            {
                bool discount = false;
                
                if (p.Discount.FirstOrDefault().EndDate > DateTime.Now)
                {
                     discount = true;
                }

                CardPack pack = new CardPack();
                pack.IdPack = p.ID;
                pack.PackName = p.Name;
                pack.IsMoney = p.IsMoney ?? model.PIsMoney;
                pack.PackPrice = (decimal)p.Price;
                pack.Flavor = p.FlavorText;
                pack.Pic = p.Image;
                pack.Worth = p.Worth ?? 0;
                pack.IsActive = (bool)p.IsActive;
                pack.ImageMimeType = p.ImageMimeType;
                pack.Pic = p.Image;
                

                Web.Models.Discount dis = new Web.Models.Discount();
                dis.StartDate = p.Discount.FirstOrDefault().StartDate?? DateTime.MinValue;
                dis.EndDate = p.Discount.FirstOrDefault().EndDate ?? DateTime.MinValue;
                dis.DiscountAmount = p.Discount.FirstOrDefault().Discount??0;

                pack.isDiscount = discount;
                pack.PackDiscount = dis;
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
            model.Userbalance = us;

            return View(model);
        }
        #endregion

        #region ACTIONRESULT ORDEROVERVIEW
        /// <summary>
        /// Return the Overview of the Orders and the Order
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public ActionResult OrderOverview()
        {
            
            log.Info("CardPackController-OrderOverview");
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
            log.Info("CardPackController-EnableCurrency");
            this.Session["isCurrency"] = true;
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
            log.Info("CardPackController-Disable Currency");
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
        [Authorize]
        public ActionResult BuyCardPack(int id)
        {
            log.Info("CardPackController-BuyCardPack");
            var dbCardPack = ShopManager.Get_CardPackById(id);

            CardPack cardPack = new CardPack();
            cardPack.IdPack = dbCardPack.ID;
            cardPack.PackName = dbCardPack.Name;
            cardPack.Worth = dbCardPack.Worth ?? 0;
            cardPack.IsMoney = (bool)dbCardPack.IsMoney;
            cardPack.NumCards = dbCardPack.NumberOfCards ?? 0;
            cardPack.PackPrice = dbCardPack.Price ?? 0;
            cardPack.Flavor = dbCardPack.FlavorText;
            cardPack.IsActive = (bool)dbCardPack.IsActive;
            cardPack.ImageMimeType = dbCardPack.ImageMimeType;
            cardPack.Pic = dbCardPack.Image;


            return View(cardPack);
        } 
        #endregion

        #region ACTIONRESULT BUY CARD PACK
        /// <summary>
        /// Takes the ID and the Number of the Packs
        /// Saves the Packs in the Database
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="numPacks"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public ActionResult BuyCardPack(int id, int numPacks)
        {
            log.Info("CardPackController-BuyCardPack");
            log.Info("id: " + id.ToString());
            log.Info("numPacks: " + numPacks.ToString());

            Order o = new Order();
            EditUserInfo ui = new EditUserInfo();
            var uiuser =UserManager.GetUserByUserEmail(User.Identity.Name);
            ui.Address = uiuser.Address;
            ui.City = uiuser.City;
            ui.Zip = uiuser.Zip??0;

            var dbCardPack = ShopManager.Get_CardPackById(id);

            CardPack cardPack = new CardPack();
           
            cardPack.IdPack = dbCardPack.ID;
            cardPack.PackName = dbCardPack.Name;
            cardPack.NumCards = dbCardPack.NumberOfCards ?? 0;
            cardPack.PackPrice = dbCardPack.Price ?? 0;
            cardPack.IsMoney = dbCardPack.IsMoney ?? false;
            cardPack.Worth = dbCardPack.Worth ?? 0;
            cardPack.IsActive = (bool)dbCardPack.IsActive;
            cardPack.ImageMimeType = dbCardPack.ImageMimeType;
            cardPack.Pic = dbCardPack.Image;
           
            o.Pack = cardPack;
            o.Quantity = numPacks;
            o.UserProfile = ui;
            
            o.CurrencyBalance = UserManager.Get_BalanceByEmail(User.Identity.Name);

            TempData["Order"] = o;
        
            return RedirectToAction("OrderOverview");
        }
        #endregion

        #region  ACTIONRESULT ORDER

        /// <summary>
        /// todo Actionresult order comment
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [ActionName("OrderOverview")]
        public ActionResult Order(Order order)
        {
            log.Info("CardPackController-Order");


                Order o = (Order)TempData["Order"];
                
            if (o==null)
            {
                return RedirectToAction("Error", "Error");
            }
                o.CardPayment = order.CardPayment;
                double priceoverall = (double)o.Pack.PackPrice * o.Quantity;
                var user = UserManager.Get_UserByEmail(User.Identity.Name);
                

            //ORDER COINS

            if (o.Pack.IsMoney == true)
            {
                try
                {

                    if (!HtmlHelpers.CreditCardValidation.IsValidCardNumber(o.CardPayment.CardNumber))
                    {
                        TempData["ConfirmMessage"] = "Kreditkartennummer bitte nochmals eingeben!";
                        return View(o);
                    }

                    //Check if Modelstate is valid
                    if (!ModelState.IsValid)
                        {
                           return View(o);

                      }
                    //Get Total Cost
                    var orderTotal = ShopManager.GetTotalCost(o.Pack.IdPack, o.Pack.Worth, o.Quantity);

                    //Check new Balance and Update Balance
                    var newBalance = o.CurrencyBalance+(o.Pack.Worth*o.Quantity);
                    var hasUpdated = UserManager.Update_BalanceByEmail(User.Identity.Name, (int)newBalance);

                    //Check if has Updated
                    if (!hasUpdated)
                    {
                        log.Error("CardPackController-Order, BalanceUpdateError");
                        return RedirectToAction("BalanceUpdateError");
                    }
                    var us = UserManager.GetUserByUserEmail(User.Identity.Name);

                    var isSaved = ShopManager.SaveOrder(us.ID, o.Pack.IdPack, (int)orderTotal, o.Quantity, o.OIsCurrency, o.CardPayment.CardCompany);
                    if (!isSaved)
                    {
                        log.Error("CardPackController-Order,SaveOrder");
                         TempData["ConfirmMessage"] = "Verbindungsproblem, bitte nochmal eingeben";
                        return RedirectToAction("OrderOverview",o);

                    }
                    TempData["ConfirmMessage"] = "Danke für deinen Einkauf";
                    var emailsend = EmailHelper.SendBillAndConfirmationAnswer(User.Identity.Name,o.Pack.IdPack, us.FirstName+" "+us.LastName, o.Pack.PackName, o.Quantity, priceoverall, us.ID,o.CardPayment.CardCompany);

                    Bill bill = new Models.Bill();
                    bill.Username=us.FirstName+" "+us.LastName;
                    bill.OrderDate = DateTime.Now;
                    bill.OrderNumber = us.ID;
                    bill.AmountOfPacks = o.Quantity;
                    bill.Packtype = o.Pack.PackName;
                    bill.Tax = (priceoverall /120)*20;
                    bill.OrderAmount = priceoverall;
                    bill.Creditcard = o.CardPayment.CardCompany;
                   
                    return View("FinalCoin",bill);

                }
                catch (Exception e)
                {
                   
                    log.Error("CardPackController-Order", e);
                    return RedirectToAction("Error", "Error");
                }
            }

            //ORDER CARDS

            else
            {

                try
                {
                    var us = UserManager.GetUserByUserEmail(User.Identity.Name);
                    var orderTotal = ShopManager.GetTotalCost(o.Pack.IdPack, o.Quantity);
                    if (orderTotal > o.CurrencyBalance)
                    {
                        TempData["ErrorMessage"] = "Nicht genug Guthaben";
                        log.Error("CardPackController-Order, NotEnoughBalance");
                        return RedirectToAction("NotEnoughBalance");
                    }
                    var newBalance = o.CurrencyBalance - orderTotal;

                    //User has enough money, subtract money
                    var hasUpdated = UserManager.Update_BalanceByEmail(User.Identity.Name, (int)newBalance);
                    if (!hasUpdated)
                    {
                        log.Error("CardPackController-Order,BalanceUpdateError");
                        return RedirectToAction("NotEnoughBalance");
                    }

                    //Generate Cards
                    var orderedCards = ShopManager.Order(o.Pack.IdPack, o.Quantity);

                    //Add Cards to User Collection
                    var hasUpdatedCards = UserManager.Add_CardsToCollectionByEmail(User.Identity.Name, orderedCards);

                    //Save Order
                    var isSaved = ShopManager.SaveOrder(us.ID, o.Pack.IdPack, (int)orderTotal, o.Quantity, o.OIsCurrency);

                    //Send Email and Confirmation
                    EmailHelper.SendEmail(User.Identity.Name, "Liebe Grüsse vom CloneShop- Team", " Deine Karten sind in der Sammlung, viel Spass beim Erstellen der Decks");
                    TempData["ConfirmMessage"] = "Danke für Ihren Einkauf";
                    TempData["OrderedCards"] = orderedCards;

                    //Redirect to ShowGeneratedCards
                    return RedirectToAction("ShowGeneratedCards");
                }
                catch (Exception e)
                {
                    
                    log.Error("CardPackController-Order Cards", e);
                    return RedirectToAction("Error", "Error");
                }
            }
        }
        #endregion


        #region FinalCoin
        public ActionResult FinalCoin(Bill o)
        {
            log.Info("CardPackController-FinalCoin");
            try
            {
                var ordered = (Order)TempData["Order"];
                return View(ordered);
            }
            catch (Exception e)
            {
                Debugger.Break();
                log.Error(e);
                return View("Error");
            }
        } 
        #endregion


        #region ACTIONRESULT SHOW GENERATED CARDS
        /// <summary>
        /// Gets the Generated Cards and returns a List
        /// of Cards to the View
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public ActionResult ShowGeneratedCards()
        {
            log.Info("CardPackController-ShowGeneratedCards");
            try
            {
                var orderedCards = (List<DAL.Model.Card>)TempData["OrderedCards"];
                var cards = new List<Web.Models.Card>();

                foreach (var c in orderedCards)
                {
                    Web.Models.Card card = new Web.Models.Card();
                    card.ID = c.ID;
                    card.Name = c.Name;
                    card.Type = c.CardType.Name;
                    card.Mana = c.ManaCost;
                    card.Attack = c.Attack;
                    card.Life = c.Life;
                    card.Pic = c.Image;
                    cards.Add(card);
                }
                return View(cards);
            }
            catch (Exception e)
            {
                Debugger.Break();
                log.Error(e);
                return View("Error");
            }
        }
        #endregion

        #region ACTIONRESULT NOT ENOUGH BALANCE
        /// <summary>
        /// Returns the "Not Enough Balance" View
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public ActionResult NotEnoughBalance()
        {
            log.Info("CardPackController-NotEnoughBalance");
            TempData["ConfirmMessage"] = "Wir leiten dich auf die Shopseite um";
            return View("OrderOverview");
        }
        #endregion

        #region DOWNLOADPDF
        public ActionResult DownloadPdf(Bill bill)
        {

            return new ActionAsPdf(
                           "CreatePdf",
                           bill)
            { FileName = "Rechnung.pdf" };
        }
        #endregion

        #region CREATEPDF
        public ActionResult CreatePdf(Bill o)
        {
            log.Info("CardPackController-FinalCoin");
            try
            {
                var order = (Bill)TempData["Order"];
                return View(o);
            }
            catch (Exception e)
            {
                Debugger.Break();
                log.Error(e);
                return View("Error");
            }
        }
        #endregion

        #region GET IMAGE
        public FileContentResult GetImage(int id)
        {
            var pack = PackManager.GetPackById(id);
            if (pack != null)
            {
                return File(pack.Image, pack.ImageMimeType);
            }
            else
            {
                return null;
            }
        } 
        #endregion
    }
}