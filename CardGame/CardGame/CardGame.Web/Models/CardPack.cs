using System.ComponentModel.DataAnnotations;


namespace CardGame.Web.Models
{
    public class CardPack
    {
        [Required]
        [Display(Name = "Nummer")]
        public int IdPack { get; set; }

        [Required]
        public string PackName { get; set; }

        
        public decimal PackPrice { get; set; }


        public string Flavor { get; set; }

 
        public bool IsMoney { get; set; }


        public byte[] Pic { get; set; }
    }
}