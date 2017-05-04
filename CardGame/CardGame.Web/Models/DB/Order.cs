

using CardGame.Resources;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace CardGame.Web.Models.DB
{
    public class Order
    {
        public CardPack Pack { get; set; }
        public int Quantity { get; set; }
        public int CurrencyBalance { get; set; }
        public bool OIsCurrency { get; set; }


        #region CARDHOLDER
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(ValidationMessages),
ErrorMessageResourceName = Constants.Validation.REQUIRED)]
        [StringLength(50,
ErrorMessageResourceType = typeof(ValidationMessages),
ErrorMessageResourceName = Constants.Validation.MAX_LENGTH)]
        [RegularExpression(@"^[a-zA-Z --]+$", ErrorMessageResourceType = typeof(ValidationMessages),
    ErrorMessageResourceName = Constants.Validation.SPECIAL_CHARACTER)]
        [Display(Name = Constants.Labels.CARD_HOLDER, ResourceType = typeof(ValidationLabels))]
        public string CardHolder { get; set; }
        #endregion

        #region CARDCOMPANY
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(ValidationMessages),
ErrorMessageResourceName = Constants.Validation.REQUIRED)]
        [StringLength(50,
ErrorMessageResourceType = typeof(ValidationMessages),
ErrorMessageResourceName = Constants.Validation.MAX_LENGTH)]
        [RegularExpression(@"^[a-zA-Z--]+$", ErrorMessageResourceType = typeof(ValidationMessages),
    ErrorMessageResourceName = Constants.Validation.SPECIAL_CHARACTER)]
        [Display(Name = Constants.Labels.CARD_COMPANY, ResourceType = typeof(ValidationLabels))]
        public string CardCompany { get; set; }
        #endregion

        #region CREDITCARDNUMBER
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(ValidationMessages),
ErrorMessageResourceName = Constants.Validation.REQUIRED)]
        [CreditCard]
        [Display(Name = Constants.Labels.CARD_NUMBER, ResourceType = typeof(ValidationLabels))]
        public string CardNumber { get; set; }
        #endregion

        #region SECURITYNUMBER
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(ValidationMessages),
ErrorMessageResourceName = Constants.Validation.REQUIRED)]
        [RegularExpression(@"^\d{9}|\d{3}-\d{2}-\d{4}$",
ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = Constants.Validation.CARD_SECURITY_N)]
        [Display(Name = Constants.Labels.CARD_SECURITY_N, ResourceType = typeof(ValidationLabels))]
        public int CardSecurityNumber { get; set; }
        #endregion

        #region EXPIRY DATE
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(ValidationMessages),
ErrorMessageResourceName = Constants.Validation.REQUIRED)]
        [RegularExpression("^(0[1-9]|1[0-2]|[1-9])\\/(1[4-9]|[2-9][0-9]|20[1-9][1-9])$",
ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = Constants.Validation.CARD_EXPIRY)]
        [DataType(DataType.Date)]
        [Display(Name = Constants.Labels.CARD_EXPIRY, ResourceType = typeof(ValidationLabels))]
        public DateTime CardExpiryDate { get; set; }
        #endregion

    }
    #region CLASS CREDIT CARD ATTRIBUTE
    internal class CreditCardAttribute : ValidationAttribute
    {
        private CardType _cardTypes;

        #region AcceptedCardTypes
        public CardType AcceptedCardTypes
        {
            get { return _cardTypes; }
            set { _cardTypes = value; }
        }
        #endregion

        #region CreditCardAttribute
        public CreditCardAttribute()
        {
            _cardTypes = CardType.All;
        }
        #endregion

        #region CreditCardAttributeII
        public CreditCardAttribute(CardType AcceptedCardTypes)
        {
            _cardTypes = AcceptedCardTypes;
        }
        #endregion

        #region IS Valid
        public override bool IsValid(object value)
        {
            var number = Convert.ToString(value);

            if (String.IsNullOrEmpty(number))
                return true;

            return IsValidType(number, _cardTypes) && IsValidNumber(number);
        }

        public override string FormatErrorMessage(string name)
        {
            return ValidationMessages.CARD_NUMBER;
        }
        #endregion

        #region ENUM CARDTYPE
        public enum CardType
        {
            Unknown = 1,
            Visa = 2,
            MasterCard = 4,
            Amex = 8,
            Diners = 16,

            All = CardType.Visa | CardType.MasterCard | CardType.Amex | CardType.Diners,
            AllOrUnknown = CardType.Unknown | CardType.Visa | CardType.MasterCard | CardType.Amex | CardType.Diners
        }
        #endregion

        #region IS VALID TYPE
        /// <summary>
        /// Checks if the Creditcardtype is valid
        /// </summary>
        /// <param name="cardNumber"></param>
        /// <param name="cardType"></param>
        /// <returns>bool</returns>
        private bool IsValidType(string cardNumber, CardType cardType)
        {
            // Visa
            if (Regex.IsMatch(cardNumber, "^(4)")
                && ((cardType & CardType.Visa) != 0))
                return cardNumber.Length == 13 || cardNumber.Length == 16;

            // MasterCard
            if (Regex.IsMatch(cardNumber, "^(51|52|53|54|55)")
                && ((cardType & CardType.MasterCard) != 0))
                return cardNumber.Length == 16;

            // Amex
            if (Regex.IsMatch(cardNumber, "^(34|37)")
                && ((cardType & CardType.Amex) != 0))
                return cardNumber.Length == 15;

            // Diners
            if (Regex.IsMatch(cardNumber, "^(300|301|302|303|304|305|36|38)")
                && ((cardType & CardType.Diners) != 0))
                return cardNumber.Length == 14;

            //Unknown
            if ((cardType & CardType.Unknown) != 0)
                return true;

            return false;
        }
        #endregion

        #region IS VALID NUMBER
        /// <summary>
        /// takes the string number and checks if it is valid
        /// </summary>
        /// <param name="number"></param>
        /// <returns>bool</returns>
        private bool IsValidNumber(string number)
        {
            int[] DELTAS = new int[] { 0, 1, 2, 3, 4, -4, -3, -2, -1, 0 };
            int checksum = 0;
            char[] chars = number.ToCharArray();
            for (int i = chars.Length - 1; i > -1; i--)
            {
                int j = ((int)chars[i]) - 48;
                checksum += j;
                if (((i - chars.Length) % 2) == 0)
                    checksum += DELTAS[j];
            }

            return ((checksum % 10) == 0);
        }
        #endregion
    }
    #endregion

}
