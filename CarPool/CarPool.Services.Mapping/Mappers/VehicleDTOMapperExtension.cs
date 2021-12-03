using CarPool.Common;
using CarPool.Data.Models.DatabaseModels;
using CarPool.Services.Mapping.DTOs;

namespace CarPool.Services.Mapping.Mappers
{
    public static class VehicleDTOMapperExtension
    {
        public static UserVehicleDTO GetDTO(this UserVehicle vehicle)
        {
            if (vehicle is null || string.IsNullOrEmpty(vehicle.Model) 
                || string.IsNullOrEmpty(vehicle.Color)
                || vehicle.FuelConsumptionPerHundredKilometers <= 0)
            {
                return new UserVehicleDTO { ErrorMessage = GlobalConstants.INCORRECT_DATA };
            }

            return new UserVehicleDTO
            {
                Id = vehicle.Id,
                Color = vehicle.Color,
                Model = vehicle.Model,
                FuelConsumptionPerHundredKilometers = vehicle.FuelConsumptionPerHundredKilometers,
                ApplicationUserId = vehicle.ApplicationUserId
            };
        }
        public static UserVehicle GetEntity(this UserVehicleDTO vehicle)
        {

            return new UserVehicle
            {
                Id = vehicle.Id,
                Color = vehicle.Color,
                Model = vehicle.Model,
                FuelConsumptionPerHundredKilometers = vehicle.FuelConsumptionPerHundredKilometers,
                ApplicationUserId = vehicle.ApplicationUserId
            };
        }
    }
}
