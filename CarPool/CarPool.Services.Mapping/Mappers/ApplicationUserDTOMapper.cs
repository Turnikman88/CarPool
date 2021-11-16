using CarPool.Common;
using CarPool.Common.Exceptions;
using CarPool.Data.Models.DatabaseModels;
using CarPool.Services.Mapping.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CarPool.Services.Mapping.Mappers
{
    public static class ApplicationUserDTOMapper
    {
        public static ApplicationUserDTO GetDTO(this ApplicationUser user)
        {
            if (user is null || user.Username is null || user.FirstName is null 
                || user.LastName is null || user.Email is null 
                || !Regex.IsMatch(user.PhoneNumber ?? "", GlobalConstants.PhoneRegex)               
                || !Regex.IsMatch(user.Password ?? "", GlobalConstants.PassRegex))
            {
                return new ApplicationUserDTO { ErrorMessage = GlobalConstants.INCORRECT_DATA };
            }

            return new ApplicationUserDTO
            {
                Id = user.Id,
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                EmailConfirmed = user.EmailConfirmed, //?
                IsBlocked = user.Ban?.BlockedOn == null ? false : true,
                PhoneNumber = user.PhoneNumber,
                Password = user.Password,
                AddressId = user.AddressId
            };
        }

        public static ApplicationUser GetEntity(this ApplicationUserDTO user)
        {            
            return new ApplicationUser
            {
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                EmailConfirmed = user.EmailConfirmed,
                PhoneNumber = user.PhoneNumber,
                Password = user.Password,
                AddressId = user.AddressId                
            };
        }

        public static ApplicationUserDisplayDTO GetDisplayDTO(this ApplicationUser user)
        {
            if (user is null || user.Username is null || user.FirstName is null
                || user.LastName is null || user.Email is null
                || !Regex.IsMatch(user.PhoneNumber ?? "", GlobalConstants.PhoneRegex)
                || !Regex.IsMatch(user.Password ?? "", GlobalConstants.PassRegex))
            {
                return new ApplicationUserDisplayDTO { ErrorMessage = GlobalConstants.INCORRECT_DATA };
            }

            return new ApplicationUserDisplayDTO
            {
                Username = user.Username,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Latitude = user.Address.Latitude,
                Longitude = user.Address.Longitude,
                Rating = user.Ratings.Count > 0 ? user.Ratings.Select(x => x.Value).ToList().Average() : 0,
                Feedbacks = user.Ratings.Select(x => x.Feedback).ToList(),
                Trips = user.Trips.Select(x => $"Start Location: {x.StartAddress.StreetName} " +
                $", End Location: {x.DestinationAddress.StreetName}, Price: {x.Price} " +
                $"Start: {x.DepartureTime.ToShortDateString()}").ToList(),
                Vehicle = user.Vehicle?.Model ?? GlobalConstants.NO_CAR_AVAILABLE,
                VehicleColor = user.Vehicle?.Color ?? GlobalConstants.NO_CAR_AVAILABLE,
                IsBlocked = user.Ban?.BlockedOn == null ? false : true,               
            };
        }
    }
}
