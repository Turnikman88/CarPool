using System.Collections.Generic;

namespace CarPool.Web.ViewModels.DTOs
{
    public class ChatViewModel
    {
        public string Sender { get; set; }
        public List<string> TripsIds { get; set; } = new List<string>();
    }
}
