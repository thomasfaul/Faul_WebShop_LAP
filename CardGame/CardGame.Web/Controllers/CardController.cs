using System.Collections.Generic;
using System.Web.Mvc;
using CardGame.Web.Models;
using CardGame.DAL.Logic;
using CardGame.DAL.Model;
using System.Linq;


namespace CardGame.Web.Controllers
{
    public class CardController : Controller
    {
        HomeController a = new HomeController();

        public int Pagesize = 100;


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
            List<Card> CardList = new List<Card>();
            var dbCardlist = CardManager.GetAllCards();
            
            foreach (var c in dbCardlist)
            {
                Card card = new Card();
                card.ID = c.idcard;
                //card.Class = c.tblclass.@class ?? 0;
                //card.Type = c.tbltype.typename;
                card.Name = c.cardname;
                card.Mana = c.mana;
                card.Attack = c.attack;
                card.Life = c.life;
                card.Pic = c.pic;
                card.Flavor = c.flavor;
                card.Class = CardManager.CardClasses[c.fkclass ?? 0];
                card.Type = CardManager.CardTypes[c.fktype];
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
            tblcard dbcard = null;

            dbcard = CardManager.GetCardById(id);

            Card card = new Card();
            card.ID = dbcard.idcard;
            card.Name = dbcard.cardname;
            card.Mana = dbcard.mana;
            card.Attack = dbcard.attack;
            card.Life = dbcard.life;
            card.Pic = dbcard.pic;
            card.Flavor = dbcard.flavor;
            //card.Class = dbcard.tblclass.@class;
            //card.Type = dbcard.tbltype.typename;
            card.Class = CardManager.CardClasses[dbcard.fkclass ?? 0];
            card.Type = CardManager.CardTypes[dbcard.fktype];
            return View(card);
        } 
        #endregion
    }
}