using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClonestoneMVC.Models
{
    public class ProfileData
    {
        public int IdUser { get; set; }

        [StringLength(maximumLength: 20, MinimumLength = 4, ErrorMessage = "password requirements not met (4-20 digits)")]
        //[Required(ErrorMessage = "required!", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        [DisplayName("Password")]
        public string Password { get; set; }

        //[Required(ErrorMessage = "required!", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "passwords not identical!")]
        [DisplayName("confirmPassword")]
        public string PasswordValidation { get; set; }

        //[Required(ErrorMessage = "required!", AllowEmptyStrings = false)]
        [DataType(DataType.EmailAddress, ErrorMessage = "invalid ermailadress")]
        [DisplayName("Email")]
        public string Email { get; set; }

        //[Required(ErrorMessage = "required!", AllowEmptyStrings = false)]
        [DataType(DataType.EmailAddress, ErrorMessage = "invalid ermailadress")]
        [System.ComponentModel.DataAnnotations.Compare("Email", ErrorMessage = "email not identical!")]
        [DisplayName("confirmEmail")]
        public string EmailValidation { get; set; }


    }
}