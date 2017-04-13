using System.ComponentModel.DataAnnotations;


namespace CardGame.Web.Models.UI
{
    public class Login : User
    {
        [Required(ErrorMessage = "Bitte geben Sie ihre Emailadresse ein", AllowEmptyStrings = false)]
        [RegularExpression(".+@.+\\..+", ErrorMessage = "Bitte gültige Emailadressedresse eingeben")]
        [Display(Name = "EmailAdresse")]
        public string Email { get; set; }
        
        [StringLength(maximumLength: 20, MinimumLength = 4, ErrorMessage = "Für ein Passwort bitte (4-20 Zeichen eingeben)")]
        [Required(ErrorMessage = "Bitte geben Sie ihr Passwort ein", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        [Display(Name = "Passwort")]
        public string Passwort { get; set; }
    }
}