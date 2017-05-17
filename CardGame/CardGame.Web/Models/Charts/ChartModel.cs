using CardGame.Web.Models.DB;
using System.Collections;
using System.Collections.Generic;

using System.Web.Helpers;

namespace CardGame.Web.Models.Charts
{
    public  class ChartViewModel
    {
        public int Wert { get; set; }
        public string Info { get; set; }
        public ArrayList Werte { get; set; }
        public ArrayList Infos { get; set; }
    }
}