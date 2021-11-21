using System;
using System.Collections.Generic;
using System.Text;

namespace CarPool.Web.ViewModels.DTOs
{
    public class ErrorDTO
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string ImageLink { get; set; }
    }
}
