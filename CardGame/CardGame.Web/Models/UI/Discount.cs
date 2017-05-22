using System;

namespace CardGame.Web.Models.UI
{
    public class Discount
    {
        public DateTime StartDate{ get; set; }
        public DateTime EndDate { get; set; }
        public int DiscountAmount { get; set; }
        public DiscountPack DiscountPack{ get; set; }
}
}