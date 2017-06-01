using System;


namespace CardGame.Web.Models.DB
{
    public class Discount: CardPack
    {
        
        public int DiscountID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Discount DiscountAmount { get; set; }
    }
}