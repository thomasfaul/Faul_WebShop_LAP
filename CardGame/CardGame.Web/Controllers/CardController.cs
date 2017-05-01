using System.Collections.Generic;
using System.Web.Mvc;
using CardGame.Web.Models;
using CardGame.DAL.Logic;
using System.Linq;
using log4net;

namespace CardGame.Web.Controllers
{
    public class CardController : Controller
    {
        HomeController a = new HomeController();

        public int Pagesize = 100;
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #region ACTIONRESULT OVERVIEW
        /// <summary>
        /// takes the cardclass und die page
        /// Orders the card by id and
        /// gives all the cards back as a 
        /// model-ActionResult
        /// </summary>
        /// <param name="cardclass"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public ActionResult Overview(int? cardclass, int page = 1)
        {
            log.Info("CardController-Overview");
            List<Web.Models.Card> CardList = new List<Web.Models.Card>();
            var dbCardlist = CardManager.GetAllCards();
            
            foreach (var c in dbCardlist)
            {
                log.Info("Deckcontroller-Editdeck");
                Web.Models.Card card = new Web.Models.Card();
                card.ID = c.ID;
                card.Name = c.Name;
                card.Mana = c.ManaCost;
                card.Attack = c.Attack;
                card.Life = c.Life;
                card.Pic = c.Image;
                card.Flavor = c.FlavorText;
                card.Class = CardManager.CardClasses[c.ID_CardClass ?? 0];
                card.Type = CardManager.CardTypes[c.ID_CardType];
                CardList.Add(card);
            }
            CardsListViewModel model = new CardsListViewModel()
            {
                Cards = CardList.OrderBy(c => c.ID)
               
            .Where(p => cardclass == null )
                           .Skip((page - 1) * Pagesize)
                           .Take(Pagesize),
                PagingInfo = new PageInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = Pagesize,
                    TotalItems = CardList.Count()
                },
                CurrentClass = cardclass
            };
            return View(model);
        }
        #endregion

        #region ActionResult DETAILS
        /// <summary>
        /// Takes the id of a card and returns 
        /// the card 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>View</returns>
        public ActionResult Details(int id)
        {
            log.Info("CardController-Details");
            DAL.Model.Card dbcard = null;

            dbcard = CardManager.GetCardById(id);

            Web.Models.Card card = new Web.Models.Card();
            card.ID = dbcard.ID;
            card.Name = dbcard.Name;
            card.Mana = dbcard.ManaCost;
            card.Attack = dbcard.Attack;
            card.Life = dbcard.Life;
            card.Pic = dbcard.Image;
            card.Flavor = dbcard.FlavorText;
            //card.Class = dbcard.tblclass.@class;
            //card.Type = dbcard.tbltype.typename;
            card.Class = CardManager.CardClasses[dbcard.ID_CardClass ?? 0];
            card.Type = CardManager.CardTypes[dbcard.ID_CardType];
            return View(card);
        } 
        #endregion
    }
}