using CarPool.Common;
using CarPool.Services.Mapping.DTOs;
using CarPool.Web.ViewModels.DTOs;

namespace CarPool.Web.ViewModels.Mappers
{
    public static class GetApplicationUserDTO
    {
        public static ApplicationUserDTO GetDTO(this RegisterDTO user)
        {
            return new ApplicationUserDTO
            {
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                IsBlocked = false,
                PhoneNumber = user.PhoneNumber,
                Password = user.Password,
                Role = GlobalConstants.UserRoleName,
                AddressId = user.AddressId
            };
        }
    }
}
