using CardGame.Resources;
using CardGame.Web.Models.UI;
using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CardGame.Web.Models.DB
{
    public class OrderInfo
    {


        #region User
        public UserInfo User { get; set; } 
        #endregion

        #region CREDITCARD
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(ValidationMessages),
ErrorMessageResourceName = Constants.Validation.REQUIRED)]
        [StringLength(50,
ErrorMessageResourceType = typeof(ValidationMessages),
ErrorMessageResourceName = Constants.Validation.MAX_LENGTH)]
        [RegularExpression(@"^[a-zA-Z--]+$", ErrorMessageResourceType = typeof(ValidationMessages),
ErrorMessageResourceName = Constants.Validation.SPECIAL_CHARACTER)]
        [Display(Name = Constants.Labels.CARD_COMPANY, ResourceType = typeof(ValidationLabels))]
        public string CreditCard { get; set; }
        #endregion

        #region ID
        [Required]
        [HiddenInput(DisplayValue = false)]
        public int ID { get; set; }
        #endregion

        #region DATE
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(ValidationMessages),
ErrorMessageResourceName = Constants.Validation.REQUIRED)]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [RegularExpression(@"^(0[1-9]|1[012])[/](0[1-9]|[12][0-9]|3[01])[/]\d{4}$", ErrorMessageResourceType = typeof(ValidationMessages),
ErrorMessageResourceName = Constants.Validation.DATE)]
        public DateTime Date { get; set; }
        #endregion

        #region isActive
        [Display(Name = Constants.Labels.ISACTIVE)]
        public bool isActive { get; set; }
        #endregion

        #region TotalCost
        [Required]
        [Range(0, 1000, ErrorMessageResourceType = typeof(ValidationMessages),
ErrorMessageResourceName = Constants.Validation.NUMBER)]
        [Display(Name = Constants.Labels.NUMBER, ResourceType = typeof(ValidationLabels))]
        public int TotalCost { get; set; }
        #endregion

        #region NumberOfPackages
        [Required]
        [Range(0, 1000, ErrorMessageResourceType = typeof(ValidationMessages),
ErrorMessageResourceName = Constants.Validation.NUMBER)]
        [Display(Name = Constants.Labels.NUMBER, ResourceType = typeof(ValidationLabels))]
        public int NumberOfPackages { get; set; }

        #endregion


    }
}