using System.Collections.Generic;


namespace CardGame.Web.Models
{
    public class PacksListViewModel
    {
        public List<CardPack> Packs { get; set; }
        public PageInfo PagingInfo { get; set; }
        public bool PIsMoney { get; set; }
       
    }
}