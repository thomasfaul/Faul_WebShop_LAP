using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace CardGame.Web.Models
{
    public class User
    {
        public int ID { get; set; }

        public string Salt { get; set; }

        public string Role { get; set; }

        [Required(ErrorMessage = "required!", AllowEmptyStrings = false)]
        [RegularExpression(@"^[a-zA-Z--]+$", ErrorMessage = "only upper and lowercase letters allowed")]
        public string Firstname { get; set; }

        [Required(ErrorMessage = "required!", AllowEmptyStrings = false)]
        [RegularExpression(@"^[a-zA-Z0-9--]+$", ErrorMessage = "no special characters allowed")]
        public string Lastname { get; set; }

        [Required(ErrorMessage = "required!", AllowEmptyStrings = false)]
        public string Gamertag { get; set; }

        [Required(ErrorMessage = "required!", AllowEmptyStrings = false)]
        [DataType(DataType.EmailAddress, ErrorMessage = "invalid emailadress")]
        [DisplayName("Email")]
        public string Email { get; set; }

        [StringLength(maximumLength: 20, MinimumLength = 4, ErrorMessage = "password requirements not met (4-20 digits)")]
        [Required(ErrorMessage = "required!", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        [DisplayName("Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "required!", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "passwords not identical!")]
        [DisplayName("confirmPassword")]
        public string PasswordValidation { get; set; }

    }
}