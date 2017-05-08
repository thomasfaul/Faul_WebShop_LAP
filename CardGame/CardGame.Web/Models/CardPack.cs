using CardGame.Resources;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CardGame.Web.Models
{
    public class CardPack
    {
        [Required]
        [HiddenInput(DisplayValue = false)]
        public int IdPack { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationMessages),
ErrorMessageResourceName = Constants.Validation.PRODUCT_NAME)]
        [Display(Name = Constants.Labels.PRODUCT_NAME, ResourceType = typeof(ValidationLabels))]
        public string PackName { get; set; }

        [Required]
<<<<<<< HEAD
        [Range(0, 20, ErrorMessageResourceType = typeof(ValidationMessages),
=======
        [Range(1, 20, ErrorMessageResourceType = typeof(ValidationMessages),
>>>>>>> origin/master
ErrorMessageResourceName = Constants.Validation.NUMBER)]
        [Display(Name = Constants.Labels.NUMBER, ResourceType = typeof(ValidationLabels))]
        public int NumCards { get; set; }

        [Required]
<<<<<<< HEAD
        [Range(0, 1000, ErrorMessageResourceType = typeof(ValidationMessages),
=======
        [Range(1, 1000, ErrorMessageResourceType = typeof(ValidationMessages),
>>>>>>> origin/master
ErrorMessageResourceName = Constants.Validation.PRICE)]
        [Display(Name = Constants.Labels.PRICE, ResourceType = typeof(ValidationLabels))]
        public decimal PackPrice { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationMessages),
ErrorMessageResourceName = Constants.Validation.DESCRIPTION)]
        [DataType(DataType.MultilineText)]
        [Display(Name = Constants.Labels.TEXT, ResourceType = typeof(ValidationLabels))]
        public string Flavor { get; set; }

        [Required]
<<<<<<< HEAD
        [Range(0, 1000, ErrorMessageResourceType = typeof(ValidationMessages),
=======
        [Range(0, 20, ErrorMessageResourceType = typeof(ValidationMessages),
>>>>>>> origin/master
ErrorMessageResourceName = Constants.Validation.NUMBER)]
        [Display(Name = Constants.Labels.WORTH, ResourceType = typeof(ValidationLabels))]
        public int Worth { get; set; }

        [Display(Name = Constants.Labels.ISMONEY, ResourceType = typeof(ValidationLabels))]
        public bool IsMoney { get; set; }

        [Display(Name =Constants.Labels.ISACTIVE)]
        public bool IsActive { get; set; }

        
        
        public byte[] Pic { get; set; }
        public string ImageMimeType { get; set; }
    }
}