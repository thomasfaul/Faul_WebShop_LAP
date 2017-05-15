using System.ComponentModel;


namespace CardGame.Web.Models
{
    public class UserProfile
    {
        public int ID { get; set; }
        [DisplayName("eMail Address")]
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string GamerTag { get; set; }
        public decimal Currency { get; set; }
        public string PassWord { get; set; }

        public int NumDistinctCardsOwned { get; set; }
        public int NumTotalCardsOwned { get; set; }
        public int NumDecksOwned { get; set; }
    }
}