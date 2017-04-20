using System.ComponentModel;

namespace CardGame.Web.Models.DB
{
    public class UserProfile
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [DisplayName("eMail Address")]
        public string Email { get; set; }
        public decimal Currency { get; set; }

        public int NumDistinctCardsOwned { get; set; }
        public int NumTotalCardsOwned { get; set; }
        public int NumDecksOwned { get; set; }
    }
}