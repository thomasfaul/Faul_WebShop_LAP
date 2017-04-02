using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CardGame.Web.Models
{
    public class PacksListViewModel
    {
        public IEnumerable<CardPack> Packs { get; set; }
        public PageInfo PagingInfo { get; set; }

        public bool? CurrentClass { get; set; }
    }
}