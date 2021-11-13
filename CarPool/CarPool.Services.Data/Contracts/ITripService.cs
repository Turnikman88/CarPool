﻿using CarPool.Services.Mapping.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarPool.Services.Data.Contracts
{
    public interface ITripService : ICRUDshared<TripDTO>
    {
        Task<TripDTO> GetTripByIDAsync(int id);
    }
}
