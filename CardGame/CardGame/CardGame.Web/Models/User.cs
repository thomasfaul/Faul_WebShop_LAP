using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CardGame.Web.Models
{
    public class User
    {
        public int ID { get; set; }

        public string Salt { get; set; }

        public string Role { get; set; }



        [Required(ErrorMessage = "bitte geben Sie ihren Vornamen ein", AllowEmptyStrings = false)]
        [Display(Name ="Vorname")]
        [RegularExpression(@"^[a-zA-Z--]+$", ErrorMessage = "Bitte keine Sonderzeichen benutzen")]
        public string Firstname { get; set; }



        [Required(ErrorMessage = "Bitte geben Sie ihren Nachnamen ein", AllowEmptyStrings = false)]
        [Display(Name ="Nachname")]
        [RegularExpression(@"^[a-zA-Z0-9--]+$", ErrorMessage = "Bitte keine Sonderzeichen benutzen")]
        public string Lastname { get; set; }



        [Required(ErrorMessage = "Bitte geben Sie Ihren Gamertag ein", AllowEmptyStrings = false)]
        [Display(Name ="Gamertag")]
        public string Gamertag { get; set; }



        [Required(ErrorMessage = "Bitte geben Sie ihre Emailadresse ein", AllowEmptyStrings = false)]
        [RegularExpression(".+@.+\\..+", ErrorMessage = "Bitte gültige Emailadressedresse eingeben")]
        [Display(Name ="EmailAdresse")]
        public string Email { get; set; }



        [StringLength(maximumLength: 20, MinimumLength = 4, ErrorMessage = "Für ein Passwort bitte (4-20 Zeichen eingeben)")]
        [Required(ErrorMessage = "Bitte geben Sie ihr Passwort ein", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        [Display(Name ="Passwort")]
        public string Passwort { get; set; }



        [StringLength(maximumLength: 20, MinimumLength = 4, ErrorMessage = "Für ein Passwort bitte (4-20 Zeichen eingeben)")]
        [Required(ErrorMessage = "Bestätigen Sie bitte ihr Passwort", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.Compare("Passwort", ErrorMessage = "Passwort ist nicht gleich")]
        [Display(Name ="Passwort bestätigen")]
        public string ConfirmPassword { get; set; }



        [Required]
        [Display(Name ="Bedingungen akzeptieren")]
        public bool TermsAccepted { get; set; }




        public string PasswordValidation { get; set; }
    }



    // IClientValidatable for client side Validation
    public class MustBeTrueAttribute : ValidationAttribute, IClientValidatable 
    {
        public override bool IsValid(object value)
        {
            return value is bool && (bool)value;
        }



        // Implement IClientValidatable for client side Validation
        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            return new ModelClientValidationRule[] { new ModelClientValidationRule { ValidationType = "checkbox", ErrorMessage = this.ErrorMessage } };
        }
    }

    // IClientValidatable for client side Validation
    public class MustBeSelected : ValidationAttribute, IClientValidatable
    {
        public override bool IsValid(object value)
        {
            if (value == null || (int)value == 0)
                return false;
            else
                return true;
        }



        // Implement IClientValidatable for client side Validation
        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            return new ModelClientValidationRule[] { new ModelClientValidationRule { ValidationType = "dropdown", ErrorMessage = this.ErrorMessage } };
        }
    }
}
 
