using CarPool.Services.Mapping.Contracts;
using System;

namespace CarPool.Services.Mapping.DTOs
{
    public class ApplicationUserDTO : IErrorMessage
    {
        public Guid Id { get; set; }

        public string Username { get; set; }

        public bool HasVehicle { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public bool IsBlocked { get; set; }

        public string PhoneNumber { get; set; }

        public string Password { get; set; }

        public int AddressId { get; set; }

        public string Role { get; set; }

        public string ErrorMessage { get; set; }

        public string ImageLink { get; set; }
    }
}

