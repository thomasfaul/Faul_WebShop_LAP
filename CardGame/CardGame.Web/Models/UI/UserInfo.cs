using CardGame.Resources;
using System.ComponentModel.DataAnnotations;


namespace CardGame.Web.Models.UI
{
    public class UserInfo
    {
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
    }  
  }