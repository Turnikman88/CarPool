using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarPool.Services.Data.Contracts
{
    public interface IBingApiService
    {
        public Task<(int,int)> GetTripDataAsync(string origin, string destination);
    }
}
