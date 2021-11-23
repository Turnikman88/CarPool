using CarPool.Common;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CarPool.Web.ViewModels.DTOs
{
    public class SettingsDTO
    {
        //profile picture
        [Required]
        [StringLength(20, MinimumLength = 2, ErrorMessage = GlobalConstants.VALUE_LENGTH_ERROR)]
        public string Username { get; set; }

        [Required]
        [DisplayName("First Name")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = GlobalConstants.VALUE_LENGTH_ERROR)]
        public string FirstName { get; set; }

        [Required]
        [DisplayName("Last Name")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = GlobalConstants.VALUE_LENGTH_ERROR)]
        public string LastName { get; set; }

        [Required]
        [DisplayName("Phone Number")]
        [RegularExpression(GlobalConstants.PhoneRegex, ErrorMessage = GlobalConstants.WRONG_PHONE)]
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = GlobalConstants.INVALID_EMAIL)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = GlobalConstants.PASSWORD_ERROR_MESSAGE)]
        public string Password { get; set; }

        [Required]
        [DisplayName("New Password")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = GlobalConstants.PASSWORD_ERROR_MESSAGE)]
        public string NewPassword { get; set; }

        [Required]
        [MinLength(5, ErrorMessage = GlobalConstants.ADDRESS_TOO_SHORT)]
        public string Address { get; set; }

        [Required]
        [MinLength(5, ErrorMessage = GlobalConstants.CITY_TOO_SHORT)]
        public string City { get; set; }

        [Required]
        public string Country { get; set; }

        public List<SelectListItem> Countries { get; set; } = new List<SelectListItem>();

        public int AddressId { get; set; }

        public string Role { get; set; }
    }
}
