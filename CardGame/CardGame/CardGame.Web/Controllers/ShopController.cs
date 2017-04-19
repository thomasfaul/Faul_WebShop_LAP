using CardGame.DAL.Logic;
using CardGame.DAL.Model;
using CardGame.Log;
using CardGame.Web.Models;
using CardGame.Web.Models.DB;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace CardGame.Web.Controllers
{
    public class ShopController : Controller
    {

        #region ACTIONRESULT INDEX
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "player")]
        public ActionResult Index()
        {
            Shop shop = new Shop();
            shop.CardPacks = new List<CardPack>();

            var dbCardPacks = ShopManager.Get_AllCardPacks();

            foreach (var dbCp in dbCardPacks)
            {
                CardPack cardPack = new CardPack();
                cardPack.IdPack = dbCp.idpack;
                cardPack.PackName = dbCp.packname;
                cardPack.NumCards = dbCp.numcards ?? 0;
                cardPack.PackPrice = dbCp.packprice ?? 0;
                shop.CardPacks.Add(cardPack);
            }
            return View(shop);
        }
        #endregion

        #region ACTIONRESULT BUY CARD PACK
        /// <summary>
        /// Takes the id of the cardpack and
        /// returns the View
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "player")]
        public ActionResult BuyCardPack(int id)
        {
            var dbCardPack = ShopManager.Get_CardPackById(id);

            CardPack cardPack = new CardPack();
            cardPack.IdPack = dbCardPack.idpack;
            cardPack.PackName = dbCardPack.packname;
            cardPack.NumCards = dbCardPack.numcards ?? 0;
            cardPack.PackPrice = dbCardPack.packprice ?? 0;

            return View(cardPack);
        }
        #endregion

        #region ACTIONRESULT BUY CARD PACK II
        /// <summary>
        /// Takes the id and the Number of the packs witch will be bought
        /// </summary>
        /// <param name="id"></param>
        /// <param name="numPacks"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "player")]
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

            o.Pack = cardPack;

            o.Quantity = numPacks;
            o.CurrencyBalance = UserManager.Get_BalanceByEmail(User.Identity.Name);

            TempData["Order"] = o;

            return RedirectToAction("OrderOverview");
        }
        #endregion

        #region ACTIONRESULT ORDEROVERVIEW
        /// <summary>
        /// Return the Overview of the Orders
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "player")]
        public ActionResult OrderOverview()
        {
            Order o = (Order)TempData["Order"];
            TempData["Order"] = o;
            return View(o);
        }
        #endregion

        #region ACTIONRESULT ORDER
        /// <summary>
        /// Checks if the User has enough balance
        /// Subtracts Money
        /// Generates Cards
        /// Adds Cards to User Collection
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "player")]
        [ActionName("OrderOverview")]
        public ActionResult Order()
        {
            Order o = (Order)TempData["Order"];
            //Check if User has enough balance
            try
            {
                var orderTotal = ShopManager.GetTotalCost(o.Pack.IdPack, o.Quantity);
                if (orderTotal > o.CurrencyBalance)
                {
                    return RedirectToAction("NotEnoughBalance");
                }
                var newBalance = Convert.ToInt32(o.CurrencyBalance - orderTotal);

                //User has enough money, subtract money
                var hasUpdated = UserManager.Update_BalanceByEmail(User.Identity.Name, newBalance);
                if (!hasUpdated)
                {
                    return RedirectToAction("BalanceUpdateError");
                }

                //Generate Cards
                var orderedCards = ShopManager.Order(o.Pack.IdPack, o.Quantity);

                //Add Cards to User Collection
                var hasUpdatedCards = UserManager.Add_CardsToCollectionByEmail(User.Identity.Name, orderedCards);

                //evtl extra spalte in cardcollection mit fk fuer den Order machen

                TempData["OrderedCards"] = orderedCards;
                return RedirectToAction("ShowGeneratedCards");
            }
            catch (Exception e)
            {
                Writer.LogError(e);
                return RedirectToAction("Error", "Error");
            }

        }
        #endregion

        #region ACTIONRESULT SHOW GENERATED CARDS
        /// <summary>
        /// Shows the bought cards, returns a view
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "player")]
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
                cards.Add(card);
            }
            //mit tempdata Orderd cards hier ansehen
            //bzw. nach aenderung einfach nur die Order ID abrufen und dann nach den jeweiligen karten suchen
            return View(cards);
        }
        #endregion

        #region ACTIONRESULT NOT ENOUGH BALANCE
        /// <summary>
        /// Shows the View Not Enough balance
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "player")]
        public ActionResult NotEnoughBalance()
        {
            return View();
        } 
        #endregion
    }
}