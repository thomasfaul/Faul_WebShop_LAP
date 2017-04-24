using System;
using System.ComponentModel.DataAnnotations;

namespace CardGame.Web.Models
{
    public class Card :IComparable<Card>
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

        public byte[] Pic { get; set; }

        public string Type { get; set; }

        public string Class { get; set; }

        public string Flavor { get; set; }

        



        public int CompareTo(Card other)
        {
            if (this.ID == other.ID)
                return 0;
            else if (this.ID< other.ID)
                return -1;
            else //this.CardID > other.CardID
                return 1;
        }
    }
}