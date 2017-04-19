using System.ComponentModel.DataAnnotations;

namespace CardGame.Web.Models
{
    public class Card
    {
        [Required]
        [Display(Name = "KartenID")]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Range(0,int.MaxValue)]
        public byte Mana { get; set; }

        [Range(0, int.MaxValue)]
        public short Attack { get; set; }

        [Range(0, int.MaxValue)]
        public short Life { get; set; }

        [Range(0, int.MaxValue)]
        public string Type { get; set; }

        public  int? CardClass { get; set; }

        public string Flavor { get; set; }

        public byte[] Pic { get; set; }
    }
}