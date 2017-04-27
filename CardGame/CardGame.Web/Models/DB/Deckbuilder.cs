using System.Collections.Generic;

namespace CardGame.Web.Models.DB
{
    public class DeckBuilder
    {
        public int DeckID { get; set; }
        public List<Card> deckcards { get; set; }
        public List<Card> collectioncards { get; set; }

        public DeckBuilder()
        {
            deckcards = new List<Card>();
            collectioncards = new List<Card>();
        }
    }
}