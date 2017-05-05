using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CardGame.Web.Models
{
    public class CardPack
    {
        [Required]
        public int IdPack { get; set; }

        [Required]
        public string PackName { get; set; }

        public int NumCards { get; set; }
      
        
        public decimal PackPrice { get; set; }


        public string Flavor { get; set; }

 
        public bool IsMoney { get; set; }

        public int Worth { get; set; }


        public byte[] Pic { get; set; }
    }
}