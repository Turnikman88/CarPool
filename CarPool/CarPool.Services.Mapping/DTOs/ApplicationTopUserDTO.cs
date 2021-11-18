﻿using CarPool.Services.Mapping.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPool.Services.Mapping.DTOs
{
    public class ApplicationTopUserDTO : IErrorMessage
    {
        public string Username { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public double Rating { get; set; }

        public string ErrorMessage { get; set; }
    }
}