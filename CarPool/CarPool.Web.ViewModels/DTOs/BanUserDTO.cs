using System;

namespace CarPool.Web.ViewModels.DTOs
{
    public class BanUserDTO
    {
        public string Email { get; set; }

        public string Reason { get; set; }

        public DateTime? Days { get; set; }
    }
}
