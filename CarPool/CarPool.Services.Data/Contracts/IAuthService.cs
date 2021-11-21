﻿using CarPool.Services.Mapping.DTOs;
using System.Threading.Tasks;

namespace CarPool.Services.Data.Contracts
{
    public interface IAuthService
    {
        Task<bool> IsExistingAsync(string email);

        Task<ResponseAuthDTO> AuthenticateAsync(RequestAuthDTO model);

        Task<ResponseAuthDTO> GetByEmailAsync(string email);

        Task<bool> IsPasswordValidAsync(string email, string password);

        Task<string> ConfirmEmail(string token);

    }
}
