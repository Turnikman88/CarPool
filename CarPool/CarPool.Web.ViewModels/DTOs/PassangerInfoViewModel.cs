using System;
using System.Collections.Generic;
using System.Text;

namespace CarPool.Web.ViewModels.DTOs
{
    public class PassangerInfoViewModel
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string ProfilePictureLink { get; set; }
    }
}
