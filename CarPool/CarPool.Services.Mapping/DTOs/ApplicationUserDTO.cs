﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CarPool.Services.Mapping.DTOs
{
    public class ApplicationUserDTO
    {
        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public double Rating { get; set; }

        public IEnumerable<string> Feedbacks { get; set; }

        public IEnumerable<string> Trips { get; set; }
    }
}

