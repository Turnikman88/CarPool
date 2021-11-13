using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarPool.Services.Data.Contracts
{
    public interface ICRUDshared<T> where T : class
    {
        Task<IEnumerable<T>> GetAsync(int page);
        Task<T> PostAsync(T obj);
        Task<T> UpdateAsync(int id, T obj);
        Task<T> DeleteAsync(int id);
    }
}
