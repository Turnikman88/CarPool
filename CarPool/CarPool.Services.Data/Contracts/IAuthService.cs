using CarPool.Services.Mapping.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarPool.Services.Data.Contracts
{
    public interface IAuthService
    {
        Task<ResponseAuthDTO> Authenticate(RequestAuthDTO model);

        Task<ResponseAuthDTO> GetByEmail(string email);
    }
}
