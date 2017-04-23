using CardGame.DAL.Logic;
using CardGame.Web.Models;
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
        [Authorize(Roles = "player,admin")]
        public ActionResult UProfile()
        {
            UserProfile profile = new UserProfile();

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
        [Authorize(Roles = "player,admin")]
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
                card.Type = cc.tbltype.typename;
                card.Pic = cc.pic;

                cardCollection.Add(card);
            }

            return View(cardCollection);
        } 
        #endregion

    }
}