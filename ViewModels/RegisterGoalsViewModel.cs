using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using HonoursProject.Models;
using HonoursProject.Views;
using Xamarin.Forms;

namespace HonoursProject.ViewModels
{

    [QueryProperty(nameof(UserGoals), nameof(UserGoals))]
    [QueryProperty(nameof(UserID), nameof(UserID))]
    public class RegisterGoalsViewModel : ViewModelBase
    {

        private List<UserGoal> userGoals;
        public List<UserGoal> UserGoals
        {
            get => userGoals;
            set
            {
                SetProperty(ref userGoals, value);
            }
        }
        private int userID;

        private User user;

        public User User
        {
            get => user;
            set => SetProperty(ref user, value);
        }
        public int UserID
        {
            get => userID;
            set
            {
                SetProperty(ref userID, value);
                SetID(value);
            }
        }

        public Command OnHealthTapped { get; }
        public Command OnSocialiseTapped { get; }
        public Command OnHabitsTapped { get; }
        public Command OnGrowthTapped { get; }
        public Command OnAddGoalsTapped { get; }
        public Command OnAddNewGoal { get; }
        public RegisterGoalsViewModel()
        {
            OnHealthTapped = new Command(OnHealth);
            OnSocialiseTapped = new Command(OnSocialise);
            //  OnHabitsTapped = new Command(OnHabits);
            OnAddNewGoal = new Command(AddNewGoal);
            OnGrowthTapped = new Command(OnGrowth);
            OnAddGoalsTapped = new Command(OnAddGoals);
        }


        public async void SetID(int value)
        {
            await TempUserGoalStore.AddUserID(value);
        }

        public async void AddUser(List<UserGoal> userGoals)
        {
            await UserStore.GetAsync(UserID);

        }

        public User GetUser()
        {
            if (User == null)
            {
                return new User();
            }
            else
            {
                return User;
            }

        }

        public async void AddNewGoal()
        {
            await Shell.Current.GoToAsync($"{nameof(AddGoalPage)}");
        }

        private async void OnHealth(object obj)
        {
            //new NavigationPage(new GoalsListPage(1));
            //      await Shell.Current.GoToAsync(nameof(TestPage));


            GoalsListPage g = new GoalsListPage();
            g.UserID = UserID;
            //g.UserD = User;
           
            await Shell.Current.GoToAsync($"{nameof(GoalsListPage)}?{nameof(GoalsListViewModel.CategoryType)}={"Health"}");

        }
        private async void OnSocialise (object obj)
        {
            await Shell.Current.GoToAsync($"{nameof(GoalsListPage)}?{nameof(GoalsListViewModel.CategoryType)}={"Socialise"}");
        }
      
        private async void OnGrowth(object obj)
        {
            await Shell.Current.GoToAsync($"{nameof(GoalsListPage)}?{nameof(GoalsListViewModel.CategoryType)}={"SelfGrowth"}");
        }
        private async void OnAddGoals(object obj)
        {
            try
            {
                //Get goals and user id
                var goals = await TempUserGoalStore.GetAllGoals();
                int id = await TempUserGoalStore.GetUserID();

                //Get user
                var User = await UserStore.GetAsync(id);

                foreach (var goal in goals)
                {
                    User.UserGoals.Add(goal);
                }

                await TempUserGoalStore.ClearAllGoals();

                //await UserStore.UpdateAsync(User,id);

                var updatedUser = await UserStore.GetAsync(id);

                await Shell.Current.GoToAsync($"//{nameof(InformationPage)}");
            }
            catch (Exception e) 
            {
              var j =  e.Message;
            }
        }

        
    }
}
