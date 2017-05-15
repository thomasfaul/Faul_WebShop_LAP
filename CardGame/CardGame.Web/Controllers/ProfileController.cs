using CardGame.DAL.Logic;
using CardGame.Web.Controllers.HtmlHelpers;
using CardGame.Web.Models;
using CardGame.Web.Models.DB;
using CardGame.Web.Models.UI;
using log4net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web;
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
            profile.ID = dbPerson.ID;
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

        #region VIEWRESULT USER EDIT
        /// <summary>
        /// Takes the UserId, gets the User
        /// from DB,returns a ViewResult with user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        public ViewResult UserEdit(int id)
        {
            log.Info("AdminController-AUSERE");
            try
            {
                AdminUserInfo u = new AdminUserInfo();
                var user = UserManager.Get_UserById(id);
                u.ID = user.ID;
                u.IsActive = user.IsActive ?? true;
                u.Firstname = user.FirstName ?? "n/a";
                u.Lastname = user.LastName ?? "n/a";
                u.Gamertag = user.GamerTag ?? "n/a";
                u.Email = user.Email ?? "n/a";
                u.CurrencyBalance = user.AmountMoney ?? 0;
                u.userrole = user.UserRole ?? "n/a";
                u.Pic = user.Avatar;
                u.ImageMimeType = user.AvatarMimeType ?? "n/a";
                u.BanDate = user.BanDate ?? DateTime.MinValue;
                u.EntryDate = user.EntryDate ?? DateTime.MinValue;
                return View(u);
            }
            catch (Exception e)
            {

                Debugger.Break();
                log.Error("AdminController-A_UserEdit", e);
                return View("Error");
            }

        }
        #endregion

        #region ACTIONRESULT A_USER EDIT II
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cp"></param>
        /// <param name="img"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public ActionResult UserEdit(AdminUserInfo au, HttpPostedFileBase img = null)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (img != null)
                    {
                        au.ImageMimeType = img.ContentType;
                        au.Pic = new byte[img.ContentLength];
                        img.InputStream.Read(au.Pic, 0, img.ContentLength);
                    }
                    UserManager.SaveAUser(au.ID, au.Firstname, au.Lastname, au.Email, au.Password, au.Gamertag, au.Pic,au.ImageMimeType);
                    TempData["message"] = string.Format("{0} wurde gespeichert", au.Lastname);
                    return RedirectToAction("A_UserIndex");
                }
                else
                {
                    return View(au);
                }
            }
            catch (Exception e)
            {
                Debugger.Break();
                log.Error("AdminController-EditII", e);
                return View("Error");
            }
        }
        #endregion
    }
}