using CardGame.Resources;
using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CardGame.Web.Models
{
    public class Card :IComparable<Card>
    {
        #region ID
        [Required]
        [HiddenInput(DisplayValue = false)]
        public int ID { get; set; } 
        #endregion

        #region NAME
        [Required(ErrorMessageResourceType = typeof(ValidationMessages),
ErrorMessageResourceName = Constants.Validation.PRODUCT_NAME)]
        [Display(Name = Constants.Labels.CARD_NAME, ResourceType = typeof(ValidationLabels))]
        public string Name { get; set; }
        #endregion

        #region MANA
        [Required(ErrorMessageResourceType = typeof(ValidationMessages),
ErrorMessageResourceName = Constants.Validation.NUMBER)]
        [Range(0, 50)]
        [Display(Name = Constants.Labels.MANA_COST, ResourceType = typeof(ValidationLabels))]
        public byte Mana { get; set; }
        #endregion

        #region ATTACK
        [Required(ErrorMessageResourceType = typeof(ValidationMessages),
ErrorMessageResourceName = Constants.Validation.NUMBER)]
        [Range(0, 50)]
        [Display(Name = Constants.Labels.ATTACK, ResourceType = typeof(ValidationLabels))]
        public short Attack { get; set; }
        #endregion

        #region LIFE
        [Required(ErrorMessageResourceType = typeof(ValidationMessages),
ErrorMessageResourceName = Constants.Validation.NUMBER)]
        [Range(0, 50)]
        [Display(Name = Constants.Labels.LIFE, ResourceType = typeof(ValidationLabels))]
        public short Life { get; set; }
        #endregion

        #region FLAVOR
        [Required(ErrorMessageResourceType = typeof(ValidationMessages),
ErrorMessageResourceName = Constants.Validation.DESCRIPTION)]
        [Display(Name = Constants.Labels.TEXT, ResourceType = typeof(ValidationLabels))]
        public string Flavor { get; set; }
        #endregion

        #region TYPE
        [Display(Name = Constants.Labels.TYPE, ResourceType = typeof(ValidationLabels))]
        public string Type { get; set; }

        #endregion


        #region ISACTIVE

        [Display(Name = Constants.Labels.ISACTIVE)]
        public bool IsActive
        { get; set;}
    #endregion

        #region IMAGE MIME TYPE
        public string ImageMimeType { get; set; }
        #endregion

        #region PIC
        public byte[] Pic { get; set; }
        #endregion



        #region COMPARE CARDS
        public int CompareTo(Card other)
        {
            if (this.ID == other.ID)
                return 0;
            else if (this.ID < other.ID)
                return -1;
            else //this.CardID > other.CardID
                return 1;
        } 
        #endregion
    }
}