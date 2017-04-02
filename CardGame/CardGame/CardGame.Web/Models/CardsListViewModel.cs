using System.Collections.Generic;

namespace CardGame.Web.Models
{
    public class CardsListViewModel
    {
        public IEnumerable<Card> Cards { get; set;}
        public PageInfo PagingInfo { get; set; }

        public int? CurrentClass{ get; set; }
    }
}