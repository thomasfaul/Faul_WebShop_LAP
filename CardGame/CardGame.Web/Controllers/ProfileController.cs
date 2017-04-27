using CardGame.DAL.Logic;
using CardGame.Web.Models;
using CardGame.Web.Models.DB;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace CardGame.Web.Controllers
{
    public class ProfileController : Controller
    {

        #region ACTIONRESULT UPROFILE
        /// <summary>
        /// Creates the ProfileView and creates the profile
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public ActionResult UProfile()
        {
            Models.UserProfile profile = new Models.UserProfile();

            var dbPerson = UserManager.Get_UserByEmail(User.Identity.Name);

            profile.Currency = (int)dbPerson.currencybalance;
            profile.Email = dbPerson.email;
            profile.FirstName = dbPerson.firstname;
            profile.LastName = dbPerson.lastname;
            profile.GamerTag = dbPerson.gamertag;


            profile.NumDistinctCardsOwned = UserManager.Get_NumDistinctCardsOwnedByEmail(User.Identity.Name);
            profile.NumDecksOwned = UserManager.Get_NumDecksOwnedByEmail(User.Identity.Name);
            profile.NumTotalCardsOwned = UserManager.Get_NumTotalCardsOwnedByEmail(User.Identity.Name);

            return View(profile);
        }
        #endregion

        #region ACTIONRESULT CARDCOLLECTION
        /// <summary>
        /// Creates the CardCollection View and
        /// sends all Cards by Email
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public ActionResult CardCollection()
        {
            var cardCollection = new List<Card>();

            var dbCardList = UserManager.Get_AllCardsByEmail(User.Identity.Name);

            foreach (var cc in dbCardList)
            {
                Card card = new Card();
                card.ID = cc.idcard;
                card.Attack = cc.attack;
                card.Name = cc.cardname;
                card.Life = cc.life;
                card.Mana = cc.mana;
                card.Flavor = cc.flavor;
                card.Pic = cc.pic;
                card.Type = UserManager.CardTypeNames[cc.fktype];
                card.Class = UserManager.CardClassNames[cc.fkclass ?? 0];

                cardCollection.Add(card);
            }

            return View(cardCollection);
        }
        #endregion


        [HttpGet]
        [Authorize]
        public ActionResult DeckOverView()
        {
            var decks = new List<Deck>();

            var dbDecks = UserManager.Get_AllDecksByEmail(User.Identity.Name);

            foreach (var d in dbDecks)
            {
                Deck deck = new Deck();
                deck.DeckID = d.iddeck;
                deck.Name = d.deckname;
                decks.Add(deck);
            }

            return View(decks);
        }

        [HttpGet]
        [Authorize]
        public ActionResult DeckDetails(int id)
        {
            var deckCards = new List<Card>();

            var dbDeckCards = DeckManager.GetDeckCardsById(id);

            foreach (var cc in dbDeckCards)
            {
                Card card = new Card();
                card.ID = cc.idcard;
                card.Attack = cc.attack;
                card.Name = cc.cardname;
                card.Life = cc.life;
                card.Mana = cc.mana;
                card.Flavor = cc.flavor;
                card.Pic = cc.pic;
                card.Type = UserManager.CardTypeNames[cc.fktype];
                //card.Class = UserManager.CardClassNames[cc.fkclass ?? 0];
                deckCards.Add(card);
            }

            return View(deckCards);
        }

    }
}