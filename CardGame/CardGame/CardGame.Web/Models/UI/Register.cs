using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CardGame.Web.Models.UI
{
    public class Register : Login
    {
        [Required(ErrorMessage = "bitte geben Sie ihren Vornamen ein", AllowEmptyStrings = false)]
        [Display(Name = "Vorname")]
        [RegularExpression(@"^[a-zA-Z--]+$", ErrorMessage = "Bitte keine Sonderzeichen benutzen")]
        public string Firstname { get; set; }



        [Required(ErrorMessage = "Bitte geben Sie ihren Nachnamen ein", AllowEmptyStrings = false)]
        [Display(Name = "Nachname")]
        [RegularExpression(@"^[a-zA-Z0-9--]+$", ErrorMessage = "Bitte keine Sonderzeichen benutzen")]
        public string Lastname { get; set; }



        [Required(ErrorMessage = "Bitte geben Sie Ihren Gamertag ein", AllowEmptyStrings = false)]
        [Display(Name = "Gamertag")]
        public string Gamertag { get; set; }

        [StringLength(maximumLength: 20, MinimumLength = 4, ErrorMessage = "Für ein Passwort bitte (4-20 Zeichen eingeben)")]
        [Required(ErrorMessage = "Bestätigen Sie bitte ihr Passwort", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.Compare("Passwort", ErrorMessage = "Passwort ist nicht gleich")]
        [Display(Name = "Passwort bestätigen")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Habe Agb´s gelesen und akzeptiere Sie hiermit")]
        [MustBeSelected]
        public bool TermsAccepted { get; set; }

        #region CLASS => ATTRIBUT [MUSTBEATTIBUTE]
            /// <summary>
            /// IClientValidatable for client side Validation
            /// </summary>
        public class MustBeTrueAttribute : ValidationAttribute, IClientValidatable
        {
            #region ISVALID
            /// <summary>
            /// Checks if it is valid
            /// </summary>
            /// <param name="value"></param>
            /// <returns></returns>
            public override bool IsValid(object value)
            {
                return value is bool && (bool)value;
            }
            #endregion

            #region GetClientValidationRules
            /// <summary>
            /// Implement IClientValidatable for client side Validation
            /// </summary>
            /// <param name="metadata"></param>
            /// <param name="context"></param>
            /// <returns></returns>
            public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
            {
                return new ModelClientValidationRule[] { new ModelClientValidationRule { ValidationType = "checkbox", ErrorMessage = this.ErrorMessage } };
            }
            #endregion
        } 
        #endregion

        #region CLASS => ATTRIBUT [MUSTBESELECTED]
        /// <summary>
        /// Hier wird das Attribut MUSTBESELECTED erstellt und überprüft
        /// </summary>
        public class MustBeSelected : ValidationAttribute, IClientValidatable
        {
            #region ISVALID 
            /// <summary>
            /// Checks if it is valid
            /// </summary>
            /// <param name="value"></param>
            /// <returns></returns>
            public override bool IsValid(object value)
            {
                if (value == null)
                    return false;
                else
                    return true;
            }
            #endregion

            #region CLIENTVALIDATION RULES
            /// <summary>
            ///  Implement IClientValidatable for client side Validation
            /// </summary>
            /// <param name="metadata"></param>
            /// <param name="context"></param>
            /// <returns></returns>
            public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
            {
                return new ModelClientValidationRule[] { new ModelClientValidationRule { ValidationType = "dropdown", ErrorMessage = this.ErrorMessage } };
            } 
            #endregion
        } 
        #endregion
    }
}