
using CarPool.Common;
using CarPool.Common.Exceptions;
using CarPool.Data.Models.DatabaseModels;
using CarPool.Services.Mapping.DTOs;
using System.Linq;
using System.Text.RegularExpressions;

namespace CarPool.Services.Mapping.Mappers
{
    public static class CityDTOMapperExtension
    {
        public static ApplicationUserDTO GetDTO(this ApplicationUser user)
        {
            if (user is null || user.FirstName is null || user.LastName is null
                || user.Username is null || user.Password is null || user.Email is null 
                || !Regex.IsMatch(user.PhoneNumber, GlobalConstants.PhoneRegex))                
            {
                throw new AppException(GlobalConstants.INCORRECT_DATA); ;
            }

            return new ApplicationUserDTO
            {
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Rating = user.Ratings.Select(x => x.Value).ToList().Average(),
                Feedbacks = user.Ratings.Select(x => x.Feedback).ToList(),
                Trips = user.Trips.Select(x => $" Id:{x.Id} ")
            };
        }

        public static ApplicationUser GetEntity(this ApplicationUserDTO ApplicationUser)
        {
            if (ApplicationUser is null || ApplicationUser.FirstName is null || ApplicationUser.LastName is null
                || ApplicationUser.Password is null || ApplicationUser.Email is null
                || !Regex.IsMatch(ApplicationUser.PhoneNumber, GlobalConstants.PhoneRegex))
            {
                throw new AppException(GlobalConstants.INCORRECT_DATA);
            }

            return new ApplicationUser
            {
                Id = ApplicationUser.Id,
                Name = ApplicationUser.Name,
                CountryId = ApplicationUser.CountryId
            };
        }
    }
}
