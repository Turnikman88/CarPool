using CarPool.Services.Mapping.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPool.Services.Mapping.DTOs
{
    public class ProfilePictureDTO : IErrorMessage
    {
        public int Id { get; set; }

        public string ImageTitle { get; set; }

        public byte[] ImageData { get; set; }

        public Guid ApplicationUserId { get; set; }

        public string ErrorMessage { get; set; }
    }
}
