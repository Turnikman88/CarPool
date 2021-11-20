using CarPool.Common;
using System.ComponentModel.DataAnnotations;

namespace CarPool.Services.Mapping.DTOs
{
    public class MailDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = GlobalConstants.INVALID_EMAIL)]
        public string Reciever { get; set; }

        public string EmailFrom { get; set; }

        public string Subject { get; set; }

        public string Phone { get; set; }

        [Required]
        public string Message { get; set; }
        public bool isSent { get; set; }
    }
}
