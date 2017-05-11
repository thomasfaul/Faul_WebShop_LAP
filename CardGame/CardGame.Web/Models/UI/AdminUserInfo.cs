using CardGame.Resources;
using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CardGame.Web.Models.UI
{
    public class AdminUserInfo
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

        #region EMAIL
   //     [Required(
   //AllowEmptyStrings = false,
   //ErrorMessageResourceType = typeof(ValidationMessages),
   //ErrorMessageResourceName = Constants.Validation.REQUIRED)]
        [StringLength(50,
    ErrorMessageResourceType = typeof(ValidationMessages),
   ErrorMessageResourceName = Constants.Validation.MAX_LENGTH)]
        [EmailAddress(ErrorMessageResourceType = typeof(ValidationMessages),
    ErrorMessageResourceName = Constants.Validation.EMAIL)]
        [Display(Name = Constants.Labels.EMAIL_LABEL, ResourceType = typeof(ValidationLabels))]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        #endregion

        #region CURRENCYBALANCE
        //[Required]
        [Range(0, 10000, ErrorMessageResourceType = typeof(ValidationMessages),
ErrorMessageResourceName = Constants.Validation.CURRENCYBALANCE)]
        [Display(Name = Constants.Labels.CURRENCYBALANCE, ResourceType = typeof(ValidationLabels))]
        public int CurrencyBalance { get; set; }
        #endregion

        #region USERROLE
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(ValidationMessages),
ErrorMessageResourceName = Constants.Validation.REQUIRED)]
        [StringLength(5,
ErrorMessageResourceType = typeof(ValidationMessages),
ErrorMessageResourceName = Constants.Validation.USERROLE)]
        [Display(Name = Constants.Labels.USERROLE, ResourceType = typeof(ValidationLabels))]
        public string userrole { get; set; }
        #endregion

        #region ISACTIVE
        [Display(Name = Constants.Labels.ISACTIVE,ResourceType = typeof(ValidationLabels))]
        public bool IsActive
        { get; set; }
        #endregion

        #region IMAGE MIME TYPE
        public string ImageMimeType { get; set; }
        #endregion

        #region PIC
        public byte[] Pic { get; set; }
        #endregion

        #region SORTVALUE
        [HiddenInput(DisplayValue = false)]
        [Display(Name ="SortValue")]
        public int? SortValue { get; set; }
        #endregion

        #region ENTRYDATE
//        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(ValidationMessages),
//ErrorMessageResourceName = Constants.Validation.REQUIRED)]
        [Display(Name = Constants.Labels.DATE, ResourceType = typeof(ValidationLabels))]
        public DateTime EntryDate { get; set; }
        #endregion

        #region BANDATE
//        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(ValidationMessages),
//ErrorMessageResourceName = Constants.Validation.REQUIRED)]
        [Display(Name = Constants.Labels.BANDATE, ResourceType = typeof(ValidationLabels))]
        public DateTime BanDate { get; set; }
        #endregion
    }
}