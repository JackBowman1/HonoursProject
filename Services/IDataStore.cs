using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HonoursProject.Services
{
    public interface IDataStore<T>
    {
       
        Task<bool> AddAsync(T info);
        Task<bool> UpdateAsync(T info);
        Task<bool> DeleteAsync(int id);
        Task<T> GetAsync(int id);
        Task<IEnumerable<T>> GetAsync(bool forceRefresh = false);
    }
}
