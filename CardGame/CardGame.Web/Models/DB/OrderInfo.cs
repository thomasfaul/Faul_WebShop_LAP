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
        [Required]
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
        [HiddenInput(DisplayValue = false)]
        public int ID { get; set; }
        #endregion

        #region DATE
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(ValidationMessages),
ErrorMessageResourceName = Constants.Validation.REQUIRED)]
        [Display(Name = Constants.Labels.DATE, ResourceType = typeof(ValidationLabels))]
        public DateTime Date { get; set; }
        #endregion

        #region isActive
        [Display(Name = Constants.Labels.ISACTIVE,ResourceType = typeof(ValidationLabels))]
        public bool isActive { get; set; }
        #endregion

        #region TotalCost
        [Required]
        [Range(0, 1000, ErrorMessageResourceType = typeof(ValidationMessages),
ErrorMessageResourceName = Constants.Validation.TOTAL_COST)]
        [Display(Name = Constants.Labels.TOTAL_COST, ResourceType = typeof(ValidationLabels))]
        public int TotalCost { get; set; }
        #endregion

        #region NumberOfPackages
        [Required]
        [Range(0, 1000, ErrorMessageResourceType = typeof(ValidationMessages),
ErrorMessageResourceName = Constants.Validation.NUMBER)]
        [Display(Name = Constants.Labels.NUMBER_PACKAGES, ResourceType = typeof(ValidationLabels))]
        public int NumberOfPackages { get; set; }

        #endregion

        [HiddenInput(DisplayValue = false)]
        public int? SortValue { get; set; }
    }
}