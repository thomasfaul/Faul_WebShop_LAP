using System.Collections.Generic;


namespace CardGame.Web.Models.DB
{
    public class Deck
    {
        public int DeckID { get; set; }
        public string Name { get; set; }
        public int NumCards { get; set; }
        public List<Card> Cards { get; set; }
        public int sortValue { get; set; }
    }
}