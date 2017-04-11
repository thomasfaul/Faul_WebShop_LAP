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
        
        // GET: Card
        public ActionResult Overview(int? cardclass,int page=1)
        {
            List<Card> CardList = new List<Card>();
            var dbCardlist = CardManager.GetAllCards();

            foreach (var c in dbCardlist)
            {
                Card card = new Card();
                card.ID = c.idcard;
                if (c.fkclass == null)
                {
                    card.CardClass = 0;
                }
                else
                {
                   card.CardClass=c.fkclass;
                }
                card.Name = c.cardname;
                card.Mana = c.mana;
                card.Attack = c.attack;
                card.Life = c.life;
                card.Pic = c.pic;
                card.Flavor = c.flavor;
                card.Type = CardManager.CardTypes[c.fktype];
                CardList.Add(card);
            }
            CardsListViewModel model = new CardsListViewModel()
            {
                Cards = CardList.OrderBy(c => c.ID)
            .Where(p => cardclass == null || p.CardClass == cardclass)
                           .Skip((page - 1) * Pagesize)
                           .Take(Pagesize),
                PagingInfo = new PageInfo {
                    CurrentPage = page,
                    ItemsPerPage = Pagesize,
                    TotalItems = CardList.Count()
                }, CurrentClass = cardclass
            };
            return View(model);
        }

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
         
            card.Type = CardManager.CardTypes[dbcard.fktype];
            return View(card);
        }
    }
}