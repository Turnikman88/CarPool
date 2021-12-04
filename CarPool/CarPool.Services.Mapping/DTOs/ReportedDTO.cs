using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

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
