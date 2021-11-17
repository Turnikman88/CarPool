using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarPool.Services.Data.Contracts
{
    public interface IBingApiService
    {
        Task<(decimal, decimal)> GetLatitudeAndLongitude(string city, string country, string street);
        Task<(int, int)> GetTripDataCityCountryAsync(string origin, string destination);
        Task<(int, int)> GetTripDataCoordinatesAsync(string originCoordinates, string destinationCoordinates);
    }
}
