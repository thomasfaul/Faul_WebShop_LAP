

using CardGame.Web.Models.UI;

namespace CardGame.Web.Models.DB
{
    public class Order
    {
        public CardPack Pack { get; set; }
        public int Quantity { get; set; }
        public int CurrencyBalance { get; set; }
        public bool OIsCurrency { get; set; }
        public Payment CardPayment {get; set;}
        public EditUserInfo UserProfile { get; set; }
    }

}
