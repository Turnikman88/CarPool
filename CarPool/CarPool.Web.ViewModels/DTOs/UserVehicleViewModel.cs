using CarPool.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CarPool.Web.ViewModels.DTOs
{
    public class UserVehicleViewModel
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Car Model")]
        [MinLength(2, ErrorMessage = GlobalConstants.MODEL_TOO_SHORT)]
        public string CarModel { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = GlobalConstants.COLOR_TOO_SHORT)]
        public string Color { get; set; }

        [Range(1, 25, ErrorMessage = GlobalConstants.FUEL_CONSUMPTION_BELOW_0)]
        public double FuelConsumptionPerHundredKilometers { get; set; }
    }
}
