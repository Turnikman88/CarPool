using CarPool.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CarPool.Web.ViewModels.DTOs
{
    public class ForgottenPasswordDTO
    {
        [Required]
        [DisplayName("Phone Number")]
        [RegularExpression(GlobalConstants.PhoneRegex, ErrorMessage = GlobalConstants.WRONG_PHONE)]
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = GlobalConstants.INVALID_EMAIL)]
        public string Email { get; set; }
    }
}
