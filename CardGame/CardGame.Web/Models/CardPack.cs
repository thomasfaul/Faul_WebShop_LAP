using CardGame.Resources;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CardGame.Web.Models
{
    public class CardPack
    {
        #region ID PACK
        [Required]
        [HiddenInput(DisplayValue = false)]
        public int IdPack { get; set; }
        #endregion

        #region PACK NAME
        [Required(ErrorMessageResourceType = typeof(ValidationMessages),
ErrorMessageResourceName = Constants.Validation.PRODUCT_NAME)]
        [Display(Name = Constants.Labels.PRODUCT_NAME, ResourceType = typeof(ValidationLabels))]
        public string PackName { get; set; }
        #endregion

        #region NUM CARDS
        [Required]
        [Range(0, 20, ErrorMessageResourceType = typeof(ValidationMessages),
ErrorMessageResourceName = Constants.Validation.NUMBER)]
        [Display(Name = Constants.Labels.NUMBER, ResourceType = typeof(ValidationLabels))]
        public int NumCards { get; set; }
        #endregion

        #region PACK PRICE
        [Required]
        [Range(0, 1000, ErrorMessageResourceType = typeof(ValidationMessages),
ErrorMessageResourceName = Constants.Validation.PRICE)]
        [Display(Name = Constants.Labels.PRICE, ResourceType = typeof(ValidationLabels))]
        public decimal PackPrice { get; set; }
        #endregion

        #region FLAVOR
        [Required(ErrorMessageResourceType = typeof(ValidationMessages),
ErrorMessageResourceName = Constants.Validation.DESCRIPTION)]
        [DataType(DataType.MultilineText)]
        [Display(Name = Constants.Labels.TEXT, ResourceType = typeof(ValidationLabels))]
        public string Flavor { get; set; }
        #endregion

        #region WORTH
        [Required]
        [Range(0, 1000, ErrorMessageResourceType = typeof(ValidationMessages),
ErrorMessageResourceName = Constants.Validation.NUMBER)]
        [Display(Name = Constants.Labels.WORTH, ResourceType = typeof(ValidationLabels))]
        public int Worth { get; set; }
        #endregion

        #region ISMONEY
        [Display(Name = Constants.Labels.ISMONEY, ResourceType = typeof(ValidationLabels))]
        public bool IsMoney { get; set; }
        #endregion

        #region ISACTIVE
        [Display(Name = Constants.Labels.ISACTIVE)]
        public bool IsActive { get; set; }
        #endregion



        #region PIC
        public byte[] Pic { get; set; }
        #endregion

        #region IMAGE MIME TYPE
        public string ImageMimeType { get; set; }
        #endregion

        [HiddenInput(DisplayValue = false)]
        public int? SortValue { get; set; }
    }
}