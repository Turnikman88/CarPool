using CarPool.Common;
using System.ComponentModel.DataAnnotations;

namespace CarPool.Web.ViewModels.DTOs
{
    public class ForgotPasswordDTO
    {
        [Required]
        [EmailAddress(ErrorMessage = GlobalConstants.INVALID_EMAIL)]
        public string Email { get; set; }

    }
}
