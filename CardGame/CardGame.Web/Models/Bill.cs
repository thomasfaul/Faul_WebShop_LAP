using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CardGame.Web.Models
{
    public class Bill
    {
        public DateTime OrderDate { get; set; }
        public double Tax { get; set; }
        public string Username { get; set; }
        public double OrderAmount { get; set; }
        public int OrderNumber { get; set; }
        public string Creditcard { get; set; }
        public string Packtype { get; set; }
        public int AmountOfPacks { get; set; }
        public bool? Value { get; set; }

    }
}