﻿

using CardGame.Resources;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace CardGame.Web.Models.DB
{
    public class Order
    {
        public CardPack Pack { get; set; }
        public int Quantity { get; set; }
        public int CurrencyBalance { get; set; }
        public bool OIsCurrency { get; set; }
    }

}
