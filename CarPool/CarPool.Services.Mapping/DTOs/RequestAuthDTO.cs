using CarPool.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CarPool.Services.Mapping.DTOs
{
    public class RequestAuthDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [RegularExpression (GlobalConstants.PassRegex, ErrorMessage = GlobalConstants.WRONG_CREDENTIALS)]
        public string Password { get; set; }
    }
}
