using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarPool.Services.Data.Contracts
{
    public interface IFuelService
    {
        Task<decimal> Price(int distance, double consumptionPer100km);
    }
}
