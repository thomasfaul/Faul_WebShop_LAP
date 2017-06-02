using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CardGame.Web.Models
{
    public class DiscountListViewModel
    {
        public List<Discount> Discounts { get; set; }
        public PageInfo PagingInfo { get; set; }
        public bool PIsMoney { get; set; }
    }
}