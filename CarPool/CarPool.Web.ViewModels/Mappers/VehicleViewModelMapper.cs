using CarPool.Services.Mapping.DTOs;
using CarPool.Web.ViewModels.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPool.Web.ViewModels.Mappers
{
    public static class VehicleViewModelMapper
    {
        public static UserVehicleDTO GetDto(this UserVehicleViewModel vehicle)
        {
            return new UserVehicleDTO
            {
                Id = vehicle.Id,
                Model = vehicle.CarModel,
                Color = vehicle.Color,
                FuelConsumptionPerHundredKilometers = vehicle.FuelConsumptionPerHundredKilometers
            };
        }
        public static UserVehicleViewModel GetViewModel(this UserVehicleDTO vehicle)
        {
            return new UserVehicleViewModel
            {
                Id = vehicle.Id,
                CarModel = vehicle.Model,
                Color = vehicle.Color,
                FuelConsumptionPerHundredKilometers = vehicle.FuelConsumptionPerHundredKilometers
            };
        }
    }
}
