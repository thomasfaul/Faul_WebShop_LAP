using CardGame.DAL.Logic;
using CardGame.DAL.Model;
using CardGame.Web.Models;
using CardGame.Web.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CardGame.Web.Controllers
{
    public class DeckController : Controller
    {
        public ActionResult EditDeck(int id)
        {

            DeckBuilder db = new DeckBuilder();
            db.DeckID = id;
            var dbDeckCards = DeckManager.GetDeckCardsById(id);
            foreach (var cc in dbDeckCards)
            {
                Card card = new Card();
                card.ID = cc.idcard;
                card.Attack = cc.attack;
                card.Name = cc.cardname;
                card.Life = cc.life;
                card.Mana = cc.mana;
                card.Type = UserManager.CardTypeNames[cc.fktype];
                //card.Class = UserManager.CardClassNames[cc.fkclass ?? 0];
                 card.Pic = cc.pic;
                card.Type = card.Type == "Minion" ? "M" : card.Type == "Spell" ? "S" : "W";
                
                db.deckcards.Add(card);
            }

            var dbCardList = UserManager.Get_AllCardsByEmail(User.Identity.Name);

            foreach (var cc in dbCardList)
            {
                Card card = new Card();
                card.ID = cc.idcard;
                card.Attack = cc.attack;
                card.Name = cc.cardname;
                card.Life = cc.life;
                card.Mana = cc.mana;
                card.Type = UserManager.CardTypeNames[cc.fktype];
                //card.Class = UserManager.CardClassNames[cc.fkclass ?? 0];
                card.Pic = cc.pic;
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

        public ActionResult AddCardToDeck(int id)
        {
            DeckBuilder db = new DeckBuilder();
            db = (DeckBuilder)TempData["DeckBuilder"];

            Card card = db.collectioncards[id];
            db.collectioncards.RemoveAt(id);

            db.deckcards.Add(card);

            db.collectioncards.Sort();
            db.deckcards.Sort();

            TempData["DeckBuilder"] = db;
            return View("EditDeck", db);
        }

        public ActionResult RemoveCardFromDeck(int id)
        {
            DeckBuilder db = new DeckBuilder();
            db = (DeckBuilder)TempData["DeckBuilder"];

            Card card = db.deckcards[id];
            db.deckcards.RemoveAt(id);

            db.collectioncards.Add(card);

            db.collectioncards.Sort();
            db.deckcards.Sort();

            TempData["DeckBuilder"] = db;
            return View("EditDeck", db);
        }

        public ActionResult SaveDeck(int id)
        {
 
            DeckBuilder db = new DeckBuilder();
            db = (DeckBuilder)TempData["DeckBuilder"];

            var dbDeckList = new List<tbldeckcard>();

            foreach (var c in db.deckcards)
            {
                int index = dbDeckList.FindIndex(i => i.tblcard.idcard == c.ID);

                if (index >= 0)
                {
                    dbDeckList[index].numcards += 1;
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
    }
}