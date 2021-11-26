using CarPool.Common;
using CarPool.Services.Mapping.DTOs;
using CarPool.Web.ViewModels.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPool.Web.ViewModels.Mappers
{
    public static class SettingsDTOMapper
    {
        public static SettingsDTO GetDTO(this ApplicationUserDTO user)
        {
            return new SettingsDTO
            {
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email
            };
        }

        public static ApplicationUserDTO GetDTO(this SettingsDTO user)
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
                Role = user.Role,
                AddressId = user.AddressId
            };
        }
    }
}
