using CardGame.DAL.Logic;
using CardGame.DAL.Model;
using CardGame.Web.Models;
using CardGame.Web.Models.DB;
using System.Collections.Generic;
using System.Web.Mvc;

namespace CardGame.Web.Controllers
{
    public class DeckController : Controller
    {
        #region ACTIONRESULT EDIT DECK
        /// <summary>
        /// Takes the deckid and inserts the deck
        /// and the collection
        /// </summary>
        /// <param name="deckid"></param>
        /// <returns></returns>
        public ActionResult EditDeck(int deckid)
        {
            Deckbuilder dbu = new Deckbuilder();
            dbu.DeckID = deckid;
            var dbdeckcards = DeckManager.GetDeckCardsById(deckid);

            foreach (var ca in dbdeckcards)
            {
                Card card = new Card();
                card.ID = ca.idcard;
                card.Life = ca.life;
                card.Mana = ca.mana;
                card.Name = ca.cardname;
                card.Type = UserManager.CardTypeNames[ca.fktype];
                card.Type = card.Type == "Minion" ? "M" : card.Type == "Spell" ? "S" : "W";
                card.CardClass = ca.tblclass.idclass;

                dbu.deckcards.Add(card);
            }
            var dbCardList = UserManager.Get_AllCardsByEmail(User.Identity.Name);

            foreach (var ca in dbCardList)
            {
                Card card = new Card();
                card.ID = ca.idcard;
                card.Attack = ca.attack;
                card.Name = ca.cardname;
                card.Life = ca.life;
                card.Mana = ca.mana;
                card.Type = UserManager.CardTypeNames[ca.fktype];
                card.Type = card.Type == "Minion" ? "M" : card.Type == "Spell" ? "S" : "W";
                card.Pic = ca.pic;

                dbu.collectioncards.Add(card);
            }
            foreach (var deckCard in dbu.deckcards)
            {
                int idx = dbu.deckcards.FindIndex(i => i.Name == deckCard.Name);
                dbu.collectioncards.RemoveAt(idx);
            }
            dbu.collectioncards.Sort();
            dbu.deckcards.Sort();
            TempData["DeckBuilder"] = dbu;
            return View(dbu);
        }
        #endregion

        #region ACTIONRESULT ADD CARD TO DECK
        /// <summary>
        /// Takes the CardId and inserts the card
        /// in the deck
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult AddCardToDeck(int id)
        {
            Deckbuilder db = new Deckbuilder();
            db = (Deckbuilder)TempData["DeckBuilder"];

            Card card = db.collectioncards[id];
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
        /// Removes a Card from the personal Deck
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult RemoveCardFromDeck(int id)
        {
            Deckbuilder db = new Deckbuilder();
            db = (Deckbuilder)TempData["DeckBuilder"];

            Card card = db.deckcards[id];
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
        /// Save the new Deck
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult SaveDeck(int id)
        {
            Deckbuilder db = new Deckbuilder();
            db = (Deckbuilder)TempData["DeckBuilder"];

            var dbDeckList = new List<tbldeckcard>();

            foreach (var c in db.deckcards)
            {
                int idx = dbDeckList.FindIndex(i => i.tblcard.idcard == c.ID);

                if (idx >= 0)
                {
                    dbDeckList[idx].numcards += 1;
                }
                else
                {
                    var dbCard = new tblcard();
                    dbCard.attack = c.Attack;
                    dbCard.cardname = c.Name;
                    dbCard.idcard = c.ID;
                    dbCard.life = c.Life;
                    dbCard.mana = c.Mana;
                    //hier einfach nur die ID befuellen (muss DB id sein)
                    //im manager dann die ID raussuchen und die db hinzufuegen
                    var dbDeckCard = new tbldeckcard();
                    dbDeckCard.tblcard = dbCard;
                    dbDeckCard.numcards = 1;
                    //dbDeckCard.tblDeck = DeckManager.GetDeckById(id);

                    dbDeckList.Add(dbDeckCard);
                }
            }

            var result = DeckManager.UpdateDeckById(id, dbDeckList); //Macht irgendwas, aber irgendwas falsches!

            //Neue DeckCollection dem DeckManager geben

            return RedirectToAction("DeckOverview", "Profile");
        } 
        #endregion
    }
}