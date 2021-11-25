using CarPool.Services.Mapping.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarPool.Services.Data.Contracts
{
    public interface ITripService : ICRUDshared<TripDTO>
    {
        Task<TripDTO> GetTripByIDAsync(int id);
        Task<IEnumerable<TripDTO>> GetTripsByUserAsync(int page, string email);
        Task<TripDTO> JoinTripAsync(int id, string userToJoinEmail);
        Task<TripDTO> LeaveTripAsync(int id, string userToLeaveEmail);
        Task<int> GetPageCountAsync();      
        Task<int> TripsCountAsync();
        Task<int> GetPageCountAsync();
        Task<int> GetPageCountPerUserAsync(string email);
    }
}
