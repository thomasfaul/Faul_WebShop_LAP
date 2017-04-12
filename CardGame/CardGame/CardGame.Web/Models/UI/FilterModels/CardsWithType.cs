using CardGame.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CardGame.Web.Models.UI.FilterModels
{
    public class CardsWithType: Card
    {
        public int IdType { get; set; }
        public string TypeName{ get; set; }

    }
}