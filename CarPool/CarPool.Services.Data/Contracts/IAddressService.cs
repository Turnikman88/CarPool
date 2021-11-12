using CarPool.Services.Mapping.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarPool.Services.Data.Contracts
{
    interface IAddressService : ICRUDshared<AddressDTO>
    {
        Task<AddressDTO> GetByIdAsync(int id);
    }
}
