using CardGame.Resources;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CardGame.Web.Models.UI
{
    public class EditUserInfo
    {
        #region ID
        [HiddenInput(DisplayValue = false)]
        public int ID { get; set; }
        #endregion

        #region FIRSTNAME
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(ValidationMessages),
    ErrorMessageResourceName = Constants.Validation.REQUIRED)]
        [StringLength(50,
    ErrorMessageResourceType = typeof(ValidationMessages),
    ErrorMessageResourceName = Constants.Validation.MAX_LENGTH)]
        [RegularExpression(@"^[a-zA-Z--]+$", ErrorMessageResourceType = typeof(ValidationMessages),
    ErrorMessageResourceName = Constants.Validation.SPECIAL_CHARACTER)]
        [Display(Name = Constants.Labels.FIRSTNAME, ResourceType = typeof(ValidationLabels))]
        public string Firstname { get; set; }
        #endregion

        #region LASTNAME
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(ValidationMessages),
ErrorMessageResourceName = Constants.Validation.REQUIRED)]
        [StringLength(50,
ErrorMessageResourceType = typeof(ValidationMessages),
ErrorMessageResourceName = Constants.Validation.MAX_LENGTH)]
        [RegularExpression(@"^[a-zA-Z--]+$", ErrorMessageResourceType = typeof(ValidationMessages),
ErrorMessageResourceName = Constants.Validation.SPECIAL_CHARACTER)]
        [Display(Name = Constants.Labels.LASTNAME, ResourceType = typeof(ValidationLabels))]
        public string Lastname { get; set; }
        #endregion

        #region GAMERTAG
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(ValidationMessages),
    ErrorMessageResourceName = Constants.Validation.REQUIRED)]
        [StringLength(50,
    ErrorMessageResourceType = typeof(ValidationMessages),
    ErrorMessageResourceName = Constants.Validation.MAX_LENGTH)]
        [Display(Name = Constants.Labels.GAMERTAG, ResourceType = typeof(ValidationLabels))]
        public string Gamertag { get; set; }
        #endregion

        #region ADDRESS
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(ValidationMessages),
ErrorMessageResourceName = Constants.Validation.REQUIRED)]
        [StringLength(50,
ErrorMessageResourceType = typeof(ValidationMessages),
ErrorMessageResourceName = Constants.Validation.MAX_LENGTH)]
        [Display(Name = Constants.Labels.ADDRESS, ResourceType = typeof(ValidationLabels))]
        public string Address { get; set; }
        #endregion

        #region EMAIL
        [Required(
   AllowEmptyStrings = false,
   ErrorMessageResourceType = typeof(ValidationMessages),
   ErrorMessageResourceName = Constants.Validation.REQUIRED)]
        [StringLength(50,
    ErrorMessageResourceType = typeof(ValidationMessages),
   ErrorMessageResourceName = Constants.Validation.MAX_LENGTH)]
        [RegularExpression(".+@.+\\..+", ErrorMessageResourceType = typeof(ValidationMessages),
    ErrorMessageResourceName = Constants.Validation.EMAIL)]
        [Display(Name = Constants.Labels.EMAIL_LABEL, ResourceType = typeof(ValidationLabels))]
        public string Email { get; set; }
        #endregion

        #region PASSWORD
        [Required(
   AllowEmptyStrings = false,
   ErrorMessageResourceType = typeof(ValidationMessages),
   ErrorMessageResourceName = Constants.Validation.REQUIRED
   )]
        [StringLength(maximumLength: 50, MinimumLength = 4,
    ErrorMessageResourceType = typeof(ValidationMessages),
   ErrorMessageResourceName = Constants.Validation.LENGTH)]
        [Display(Name = Constants.Labels.PASSWORD, ResourceType = typeof(ValidationLabels))]
        public string Password { get; set; }
        #endregion

        #region PASSWORDCONFIRMATION
        [Required(
        AllowEmptyStrings = false,
        ErrorMessageResourceType = typeof(ValidationMessages),
        ErrorMessageResourceName = Constants.Validation.REQUIRED
        )]
        //[DataType(DataType.Password)]
        [StringLength(50,
     ErrorMessageResourceType = typeof(ValidationMessages),
    ErrorMessageResourceName = Constants.Validation.MAX_LENGTH)]
        [System.ComponentModel.DataAnnotations.Compare(nameof(Password),
    ErrorMessageResourceType = typeof(ValidationMessages),
    ErrorMessageResourceName = Constants.Validation.PASSWORD_EQUAL)]
        [Display(Name = Constants.Labels.CONFIRMATION, ResourceType = typeof(ValidationLabels))]
        public string PasswordConfirmation { get; set; }
        #endregion

        #region CITY
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(ValidationMessages),
ErrorMessageResourceName = Constants.Validation.REQUIRED)]
        [StringLength(50,
ErrorMessageResourceType = typeof(ValidationMessages),
ErrorMessageResourceName = Constants.Validation.MAX_LENGTH)]
        [Display(Name = Constants.Labels.CITY, ResourceType = typeof(ValidationLabels))]
        public string City { get; set; }
        #endregion

        #region ZIP
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(ValidationMessages),
ErrorMessageResourceName = Constants.Validation.REQUIRED)]
        [Display(Name = Constants.Labels.ZIP, ResourceType = typeof(ValidationLabels))]
        public int Zip { get; set; }
        #endregion

        #region IMAGE MIME TYPE
        public string ImageMimeType { get; set; }
        #endregion

        #region PIC
        public byte[] Pic { get; set; }
        #endregion
    }
}