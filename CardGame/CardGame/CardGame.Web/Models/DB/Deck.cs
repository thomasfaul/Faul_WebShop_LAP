
using System.Collections.Generic;


namespace CardGame.Web.Models.DB
{
    public class Deck
    {
        public int DeckID { get; set; }
        public string DeckName { get; set; }
        public List<Card> DeckCards { get; set; }
    }
}