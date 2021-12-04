using CarPool.Services.Mapping.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPool.Web.ViewModels.DTOs
{
    public class BanViewModel
    {
        public IEnumerable<BanDTO> Banned { get; set; }

        public int MaxPages { get; set; }

        public int CurrentPage { get; set; }
    }
}
