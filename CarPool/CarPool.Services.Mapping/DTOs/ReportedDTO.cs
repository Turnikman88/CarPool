using System;
using System.Collections.Generic;
using System.Text;

namespace CarPool.Services.Mapping.DTOs
{
    public class ReportedDTO
    {
        public string Email { get; set; }

        public string Picture { get; set; }

        public string Reason { get; set; }

        public int? Days { get; set; }
    }
}
