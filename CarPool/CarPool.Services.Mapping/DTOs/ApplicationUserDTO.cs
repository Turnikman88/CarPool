using CarPool.Common;
using CarPool.Services.Mapping.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPool.Services.Mapping.DTOs
{
    public class ApplicationUserDTO : IErrorMessage
    {
        public Guid Id { get; set; }
        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public bool EmailConfirmed { get; set; }

        public bool IsBlocked { get; set; }

        public string PhoneNumber { get; set; }
             
        public string Password { get; set; }
        public string ErrorMessage { get; set; }
    }
}

