using CarPool.Common;
using System.ComponentModel.DataAnnotations;

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
