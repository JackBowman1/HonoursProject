using HonoursProject.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace HonoursProject.ViewModels
{
    public class AddGoalViewModel : ViewModelBase
    {
        private string goalName;
        public string GoalName 
        {
            get => goalName;
            set => SetProperty(ref goalName, value);
        }
        public Command AddGoalCommand { get; }
        public AddGoalViewModel() 
        {
            AddGoalCommand = new Command(AddNewGoal);
        }

        public async void AddNewGoal() 
        {
            string name = GoalName;

            if((name == null) || name.Equals(""))
            {
                await App.Current.MainPage.DisplayAlert("Error adding goal:", "You have not entered a goal name", "Ok");
            }else 
            {
                int id = await TempUserGoalStore.GetUserID();

                UserGoal userGoal = new UserGoal();
                userGoal = new UserGoal { GoalName = name };

                User user = await UserStore.GetAsync(id);
                user.UserGoals.Add(userGoal);

                await App.Current.MainPage.DisplayAlert("Goal added successfully:", "You have added a new goal", "Ok");
                GoalName = "";
            }
        }
    }
}
