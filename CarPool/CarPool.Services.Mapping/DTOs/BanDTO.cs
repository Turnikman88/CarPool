using CarPool.Services.Mapping.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPool.Services.Mapping.DTOs
{
    public class BanDTO : IErrorMessage
    {
        public int Id { get; set; }

        public DateTime? BlockedOn { get; set; }

        public DateTime? BlockedDue { get; set; }

        public Guid ApplicationUserId { get; set; }

        public string UserEmail { get; set; }

        public string Reason { get; set; }

        public string ErrorMessage { get; set; }

        public string Picture { get; set; }

        public string BanRemovedMessage { get; set; }
    }
}
