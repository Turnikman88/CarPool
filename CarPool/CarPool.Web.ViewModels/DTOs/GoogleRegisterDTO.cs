using CarPool.Common;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CarPool.Web.ViewModels.DTOs
{
    public class GoogleRegisterDTO
    {
        [Required]
        [DisplayName("Phone Number")]
        [RegularExpression(GlobalConstants.PhoneRegex, ErrorMessage = GlobalConstants.WRONG_PHONE)]
        public string PhoneNumber { get; set; }

        [Required]
        [MinLength(5, ErrorMessage = GlobalConstants.ADDRESS_TOO_SHORT)]
        public string Address { get; set; }

        [Required]
        [MinLength(5, ErrorMessage = GlobalConstants.ADDRESS_TOO_SHORT)]
        public string City { get; set; }

        [Required]
        public string Country { get; set; }

        public List<SelectListItem> Countries { get; set; } = new List<SelectListItem>();

    }
}
