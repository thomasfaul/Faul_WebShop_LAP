﻿
namespace CardGame.Web.Models
{
    public class User
    {
        public int ID { get; set; }

        public string Salt { get; set; }

        public string Role { get; set; }
        
        public int Currencybalance { get; set; }
    }
}
 