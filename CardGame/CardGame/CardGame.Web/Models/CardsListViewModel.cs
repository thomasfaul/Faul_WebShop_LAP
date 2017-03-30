using System.Collections.Generic;
using CardGame.DAL;

namespace CardGame.Web.Models
{
    public class CardsListViewModel
    {
        public IEnumerable<Card> Cards { get; set;}
        public PageInfo PagingInfo { get; set; }
    }
}