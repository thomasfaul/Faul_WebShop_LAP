using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CardGame.Web.Models
{
    public class Discount
    {
        public int ID { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime StartDate { get; set; }
        public int DiscountAmount { get; set; }
        public CardPack DiscountPack { get; set; }
    }
}