using System;
using System.ComponentModel.DataAnnotations;

namespace CarPool.Services.Mapping.DTOs
{
    public class ReportedDTO
    {
        public string Email { get; set; }

        public string Picture { get; set; }

        public string Reason { get; set; }

        [DataType(DataType.Date)]
        public DateTime? Days { get; set; }

        public string Message { get; set; }
    }
}
