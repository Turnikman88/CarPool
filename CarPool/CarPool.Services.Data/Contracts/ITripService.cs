using CarPool.Services.Mapping.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarPool.Services.Data.Contracts
{
    public interface ITripService : ICRUDshared<TripDTO>
    {
        Task<TripDTO> GetTripByIDAsync(int id);
        Task<IEnumerable<TripDTO>> GetAllUpcomingTripsAsync(int page);
        Task<IEnumerable<TripDTO>> GetUpcomingTripsByUserAsync(int page, string email);
        Task<IEnumerable<TripDTO>> GetPastByUserTrips(int page, string email);
        Task<IEnumerable<TripDriverDTO>> GetUpcomingTripsByUserAsDriverAsync(int page, string email);
        Task<TripDTO> JoinTripAsync(int id, string userToJoinEmail);
        Task<TripDTO> LeaveTripAsync(int id, string userToLeaveEmail);
        Task<int> GetPageCountAsync();
        Task<int> TripsCountAsync();
        Task<bool> KickUserAsync(string userId, int tripId, string requestEmail);
        Task<int> GetPageCountPerUserAsync(string email);
    }
}
