using CardGame.DAL.Logic;
using CardGame.Web.Controllers.HtmlHelpers;
using CardGame.Web.Models;
using CardGame.Web.Models.DB;
using log4net;
using System.Collections.Generic;
using System.Web.Mvc;

namespace CardGame.Web.Controllers
{
    public class ProfileController : Controller
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #region ACTIONRESULT UPROFILE
        /// <summary>
        /// Creates the ProfileView and creates the profile
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public ActionResult UProfile()
        {
            log.Info("ProfileController-UProfile");
            Models.UserProfile profile = new Models.UserProfile();

            var dbPerson = UserManager.Get_UserByEmail(User.Identity.Name);

            profile.Currency = (int)dbPerson.AmountMoney;
            profile.Email = dbPerson.Email;
            profile.FirstName = dbPerson.FirstName;
            profile.LastName = dbPerson.LastName;
            profile.GamerTag = dbPerson.GamerTag;


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
        public ActionResult CardCollection(int? sortValue, string search)
        {
           
        log.Info("ProfileController-CardCollection");
            var cardCollection = new List<Card>();

            var dbCardList = UserManager.Get_AllCardsByEmail(User.Identity.Name);

            foreach (var cc in dbCardList)
            {
                Card card = new Card();
                card.ID = cc.ID;
                card.Attack = cc.Attack;
                card.Name = cc.Name;
                card.Life = cc.Life;
                card.Mana = cc.ManaCost;
                card.Flavor = cc.FlavorText;
                card.Pic = cc.Image;
                card.Type = UserManager.CardTypeNames[cc.ID_CardType];
                //card.Class = UserManager.CardClassNames[cc.ID_CardClass ?? 0 ];

                cardCollection.Add(card);
            }

            if (sortValue != null)
            {
                cardCollection = SortHelper.FilterCards(cardCollection, (int)sortValue);
            }
            if (search != null)
            {
                cardCollection = SortHelper.FilterCards(cardCollection, search);
            }



            return View(cardCollection);
        }
        #endregion

        #region ACTIONRESULT DECKOVERVIEW
        /// <summary>
        /// Returns the Deckview of a User
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public ActionResult DeckOverView()
        {
            log.Info("ProfileController-DeckOverView");
            var decks = new List<Deck>();

            var dbDecks = UserManager.Get_AllDecksByEmail(User.Identity.Name);

            foreach (var d in dbDecks)
            {
                Deck deck = new Deck();
                deck.DeckID = d.ID;
                deck.Name = d.Name;
                decks.Add(deck);
            }

            return View(decks);
        }
        #endregion

        #region ACTIONRESULT DECKDETAILS
        [HttpGet]
        [Authorize]
        public ActionResult DeckDetails(int id)
        {
            log.Info("ProfileController-DeckDetails");
            var deckCards = new List<Card>();

            var dbDeckCards = DeckManager.GetDeckCardsById(id);

            foreach (var cc in dbDeckCards)
            {
                Card card = new Card();
                card.ID = cc.ID;
                card.Attack = cc.Attack;
                card.Name = cc.Name;
                card.Life = cc.Life;
                card.Mana = cc.ManaCost;
                card.Flavor = cc.FlavorText;
                card.Pic = cc.Image;
                card.Type = UserManager.CardTypeNames[cc.ID_CardType];
                //card.Class = UserManager.CardClassNames[cc.CardClass.ID];
                deckCards.Add(card);
            }

            return View(deckCards);
        } 
        #endregion

    }
}