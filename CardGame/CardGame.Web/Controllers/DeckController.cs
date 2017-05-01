using CardGame.DAL.Logic;
using CardGame.DAL.Model;
using CardGame.Web.Models.DB;
using log4net;
using System.Collections.Generic;
using System.Web.Mvc;

namespace CardGame.Web.Controllers
{
    public class DeckController : Controller
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #region ACTIONRESULT EDITDECK
        /// <summary>
        /// Takes the Id of the Deck
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult EditDeck(int id)
        {
            log.Info("Deckcontroller-Editdeck");

            DeckBuilder db = new DeckBuilder();
            db.DeckID = id;
            var dbDeckCards = DeckManager.GetDeckCardsById(id);
            foreach (var cc in dbDeckCards)
            {
                Web.Models.Card card = new Web.Models.Card();
                card.ID = cc.ID;
                card.Attack = cc.Attack;
                card.Name = cc.Name;
                card.Life = cc.Life;
                card.Mana = cc.ManaCost;
                card.Type = UserManager.CardTypeNames[cc.ID_CardType];
                card.Pic = cc.Image;
                card.Type = card.Type == "Minion" ? "M" : card.Type == "Spell" ? "S" : "W";

                db.deckcards.Add(card);
            } 
            

            var dbCardList = UserManager.Get_AllCardsByEmail(User.Identity.Name);

            foreach (var cc in dbCardList)
            {
                Web.Models.Card card = new Web.Models.Card();
                card.ID = cc.ID;
                card.Attack = cc.Attack;
                card.Name = cc.Name;
                card.Life = cc.Life;
                card.Mana = cc.ManaCost;
                card.Type = UserManager.CardTypeNames[cc.ID_CardType];
                //card.Class = UserManager.CardClassNames[cc.fkclass ?? 0];
                card.Pic = cc.Image;
                card.Type = card.Type == "Minion" ? "M" : card.Type == "Spell" ? "S" : "W";

                db.collectioncards.Add(card);
            }

            foreach (var deckCard in db.deckcards)
            {
                int index = db.collectioncards.FindIndex(i => i.Name == deckCard.Name);
                db.collectioncards.RemoveAt(index);
            }

            db.collectioncards.Sort();
            db.deckcards.Sort();

            TempData["DeckBuilder"] = db;
            return View(db);
        }
        #endregion

        #region ACTIONRESULT ADD CARD TO DECK
        /// <summary>
        /// Adds a Card to the Deck
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult AddCardToDeck(int id)
        {
            log.Info("Deckcontroller-AddCardToDeck");
            DeckBuilder db = new DeckBuilder();
            db = (DeckBuilder)TempData["DeckBuilder"];

            Web.Models.Card card = db.collectioncards[id];
            db.collectioncards.RemoveAt(id);

            db.deckcards.Add(card);

            db.collectioncards.Sort();
            db.deckcards.Sort();

            TempData["DeckBuilder"] = db;
            return View("EditDeck", db);
        }
        #endregion

        #region ACTIONRESULT REMOVE CARD FROM DECK
        /// <summary>
        /// Takes the id of the Card and removes it from
        /// the Deck
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult RemoveCardFromDeck(int id)
        {
            log.Info("Deckcontroller-RemoveCardFromDeck");
            DeckBuilder db = new DeckBuilder();
            db = (DeckBuilder)TempData["DeckBuilder"];

            Web.Models.Card card = db.deckcards[id];
            db.deckcards.RemoveAt(id);

            db.collectioncards.Add(card);

            db.collectioncards.Sort();
            db.deckcards.Sort();

            TempData["DeckBuilder"] = db;
            return View("EditDeck", db);
        }
        #endregion

        #region ACTIONRESULT SAVE DECK
        /// <summary>
        /// Saves A Deck
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult SaveDeck(int id)
        {
            log.Info("Deckcontroller-SaveDeck");
            DeckBuilder db = new DeckBuilder();
            db = (DeckBuilder)TempData["DeckBuilder"];

            var dbDeckList = new List<DeckCard>();

            foreach (var c in db.deckcards)
            {
                int index = dbDeckList.FindIndex(i => i.Card.ID == c.ID);

                if (index >= 0)
                {
                    dbDeckList[index].NumberOfCards += 1;
                }
                else
                {
                    var dbCard = new DAL.Model.Card();
                    dbCard.Attack = c.Attack;
                    dbCard.Name = c.Name;
                    dbCard.ID = c.ID;
                    dbCard.Life = c.Life;
                    dbCard.ManaCost = c.Mana;
                    dbCard.FlavorText = c.Flavor;
                    var dbDeckCard = new DeckCard();
                    dbDeckCard.Card = dbCard;
                    dbDeckCard.NumberOfCards = 1;
                    dbDeckCard.Deck = DeckManager.GetDeckById(id);

                    dbDeckList.Add(dbDeckCard);
                }
            } 
          

            var result = DeckManager.UpdateDeckById(id, dbDeckList); 
            return RedirectToAction("DeckOverview", "Profile");
        }
        #endregion
    }
}