using CarPool.Services.Mapping.DTOs;
using System.Threading.Tasks;

namespace CarPool.Services.Data.Contracts
{
    public interface ITripService : ICRUDshared<TripDTO>
    {
        Task<TripDTO> GetTripByIDAsync(int id);
        Task<TripDTO> JoinTripAsync(int id, string userToJoinEmail);
        Task<TripDTO> LeaveTripAsync(int id, string userToLeaveEmail);
        Task<int> GetPageCountAsync();
    }
}
