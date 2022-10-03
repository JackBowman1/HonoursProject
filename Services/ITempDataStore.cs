using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HonoursProject.Services
{
    public interface ITempDataStore<T>
    {
        Task<bool> AddUserID(int id);
        Task<int> GetUserID();
        Task<bool> LogOutUser();
        Task<bool> RemoveGoal(T item);

        Task<bool> AddGoal(T item);
        Task<bool> ClearAllGoals();
        Task<bool> UpdateGoal(T item);
        Task<T> GetGoal(int id);
        Task<IEnumerable<T>> GetAllGoals(bool forceRefresh = false);
    }
}
