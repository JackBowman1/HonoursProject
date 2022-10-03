using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using HonoursProject.Models;

namespace HonoursProject.Services
{
    public class GoalStore : IDataStore<UserGoal>
    {
        readonly List<UserGoal> UserGoals = new List<UserGoal>();
        public GoalStore() 
        {
           UserGoals = InitialiseGoals();
        
        }
        public async Task<bool> AddAsync(UserGoal userGoal)
        {
            UserGoals.Clear();
            UserGoals.Add(userGoal);

            return await Task.FromResult(true);
        }


        public async Task<bool> UpdateAsync(UserGoal userGoal)
        {
            var oldDiary = UserGoals.Where((UserGoal arg) => arg.Id == userGoal.Id).FirstOrDefault();
            UserGoals.Remove(oldDiary);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var oldInfo = UserGoals.Where((UserGoal arg) => arg.Id == id).FirstOrDefault();

            return await Task.FromResult(true);
        }

        public async Task<UserGoal> GetAsync(int id)
        {
            return await Task.FromResult(UserGoals.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<UserGoal>> GetAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(UserGoals);
        }

        public async Task<IEnumerable<UserGoal>> GetUserStoreType(string type)
        {
            return await Task.FromResult(UserGoals.FindAll(s => s.Type == type));
        }

        public List<UserGoal> InitialiseGoals()
        {
            List<UserGoal> userGoals = new List<UserGoal>()
            {
                new UserGoal
                {
                    Id = 1,
                    GoalName = "Join an online excercise class",
                    Description = "Excercise is great for your physical and mental health. Joining an online exercise class can be a great way to stay active in the comfort of your own home. It can also be a great way to meet new people and create new bonds",
                    Type = "Health"
                },
                 new UserGoal
                {
                    Id = 2,
                    GoalName = "Eat healthier",
                    Description = "Having a well balanced is shown to be important in maintaining your physical and mental health. Eating foods such as fruits and vegetables will help improve your overall mood and well-being.",
                    Type = "Health"
                },
                  new UserGoal
                {
                    Id = 3,
                    GoalName= "Get 8 hours of sleep",
                    Description = "Getting the right amount of sleep can come with a range of benefits including a better immune system, reduce your stress levels and maintain good relationships with friends or family",
                    Type = "Health"
                },
                   new UserGoal
                {
                    Id = 4,
                    GoalName = "Go for a walk",
                    Description = "A walk can be a great way to stay healthy, increase your energy levels and clear your mind.",
                    Type = "Health"
                },
                new UserGoal
                {
                    Id = 5,
                    GoalName = "Join an online group",
                    Description = "Joining an online group can be an good way to make new friends, whilst at the same time enjoying the group activities.",
                    Type = "Socialise"
                },
                new UserGoal
                {
                    Id = 6,
                    GoalName = "Talk to friends or family",
                    Description = "Friends and family can have a major impact on your on your health and well-being. Simply just talking to an old friend or family member can help boost your hapiness and reduce stress",
                    Type = "Socialise"
                },
                new UserGoal
                {
                    Id = 7,
                    GoalName = "Make a new friendship",
                    Description = "Friends provide a big role in improving your overall health. You can make new friends by staying in touch with people with whom you have taken a class with or worked with. Introducing yourself to neighbours can also be a good way. ",
                    Type = "Socialise"
                },
                new UserGoal
                {
                    Id = 8,
                    GoalName = "Volunteer",
                    Description = "Volunteering is a great way to give something back to your community. It will give you a chance to meet new people, learn new skills and help people. ",
                    Type = "Socialise"
                },
                new UserGoal
                {
                    Id = 100,
                    GoalName = "",
                    Type = "Habits"
                },
                new UserGoal
                {
                    Id = 9,
                    GoalName = "Read a book",
                    Description = "Reading a book improves your brain, can help improve sleep, reduce stress and learn something interesting.",
                    Type = "SelfGrowth"
                },
                new UserGoal
                {
                    Id = 10,
                    GoalName = "Learn something new",
                    Description = "Taking on the challenge of learning something new such as new language can be a good way to increase cognitive abilities, gain confidence and have fun.  ",
                    Type = "SelfGrowth"
                },
                new UserGoal
                {
                    Id = 11,
                    GoalName = "Learn an instrument",
                    Description = "Learning an instrument will give the sense of acomplishment, improve your patience and increase your creativity. ",
                    Type = "SelfGrowth"
                },
                new UserGoal
                {
                    Id = 12,
                    GoalName = "Create a to do list",
                    Description = "Creating a to do list can help you achieve your goals, improve memory and increase motivation and productivity. ",
                    Type = "SelfGrowth"
                },
            };

            return userGoals;
        }


    }
}
