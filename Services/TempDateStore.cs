using HonoursProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HonoursProject.Services
{
    public class TempDateStore : ITempDataStore<UserGoal>
    {
      
        readonly List<UserGoal> UserGoals = new List<UserGoal>();
        readonly List<int> UserID = new List<int>();

        public async Task<bool> AddUserID(int id)
        {

           UserID.Add(id);
           return await Task.FromResult(true);
            
        }

        public async Task<bool> RemoveGoal(UserGoal goal) 
        {
            UserGoals.Remove(goal);
            int i = UserGoals.Count;
            return await Task.FromResult(true);
        }

        public async Task<bool> LogOutUser() 
        {
            UserID.Clear();
            return await Task.FromResult(true);
        }


        public async Task<int> GetUserID() 
        {
            int id = UserID.FirstOrDefault();
            return await Task.FromResult(id);
            
        }

        public async Task<bool> AddGoal(UserGoal userGoal)
        {
            UserGoals.Add(userGoal);

            return await Task.FromResult(true);

        }

        public async Task<bool> ClearAllGoals()
        {
            UserGoals.Clear();
            return await Task.FromResult(true);

        }

        public async Task<UserGoal> GetGoal(int id)
        {
            return await Task.FromResult(UserGoals.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<UserGoal>> GetAllGoals(bool forceRefresh = false)
        {
            return await Task.FromResult(UserGoals);
        }

        public async Task<bool> UpdateGoal(UserGoal userGoal)
        {

            var oldUserGoal = UserGoals.Where((UserGoal arg) => (arg.Id == userGoal.Id)).FirstOrDefault();
            UserGoals.Remove(oldUserGoal);

            return await Task.FromResult(true);
        }

       
    }
}
