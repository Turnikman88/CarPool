using System;

namespace CarPool.Services.Mapping.DTOs
{
    public class InboxDTO
    {
        public string Author { get; set; }

        public string Recipient { get; set; }

        public DateTime SendOnDate { get; set; }

        public string Message { get; set; }
    }
}
