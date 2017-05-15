using CardGame.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
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
        #region IMAGE MIME TYPE
        public string ImageMimeType { get; set; }
        #endregion
        #region PIC
        public byte[] Pic { get; set; }
        #endregion
    }
}