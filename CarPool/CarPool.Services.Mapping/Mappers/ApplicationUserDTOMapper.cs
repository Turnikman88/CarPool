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
                || !Regex.IsMatch(user.PhoneNumber, GlobalConstants.PassRegex)               
                || user.Password is null)
            {
                throw new AppException(GlobalConstants.INCORRECT_DATA); ;
            }

            return new ApplicationUserDTO
            {
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                EmailConfirmed = user.EmailConfirmed,
                PhoneNumber = user.PhoneNumber,
                Password = user.Password                             
            };
        }

        public static ApplicationUser GetEntity(this ApplicationUserDTO user)
        {
            if (user is null || user.Username is null || user.FirstName is null
                || user.LastName is null || user.Email is null
                || !Regex.IsMatch(user.PhoneNumber, GlobalConstants.PassRegex)
                || user.Password is null)
            {
                throw new AppException(GlobalConstants.INCORRECT_DATA); ;
            }

            return new ApplicationUser
            {
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                EmailConfirmed = user.EmailConfirmed,
                PhoneNumber = user.PhoneNumber,
                Password = user.Password
            };
        }

        public static ApplicationUserDisplayDTO GetDisplayDTO(this ApplicationUser user)
        {
            if (user is null || user.Username is null || user.FirstName is null
                || user.LastName is null || user.Email is null
                || !Regex.IsMatch(user.PhoneNumber, GlobalConstants.PassRegex)
                || user.Password is null)
            {
                throw new AppException(GlobalConstants.INCORRECT_DATA); ;
            }

            return new ApplicationUserDisplayDTO
            {
                Username = user.Username,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Latitude = user.Address.Latitude,
                Longitude = user.Address.Longitude,
                Rating = user.Ratings.Select(x => x.Value).ToList().Average(),
                Feedbacks = user.Ratings.Select(x => x.Feedback).ToList(),
                Trips = user.Trips.Select(x => $"Start Location: {x.StartAddress}, End Location: {x.DestinationAddress}, Price: {x.Price}" +
                $"Start: {x.DepartureTime.ToShortDateString()}"),
                Vehicle = user.Vehicle.Model,
                VehicleColor = user.Vehicle.Color,
                IsBlocked = user.Ban.BlockedOn == null ? false : true,               
            };
        }
    }
}
