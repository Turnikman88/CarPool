using System.Threading.Tasks;

namespace CarPool.Services.Data.Contracts
{
    public interface IBingApiService
    {
        Task<(decimal, decimal)> GetLatitudeAndLongitude(string city, string country, string street);
        Task<(int, int)> GetTripDataByCityCountryAsync(string origin, string destination);
        Task<(int, int)> GetTripDataByCoordinatesAsync(string originCoordinates, string destinationCoordinates);
    }
}
