using HonoursProject.Models;
using HonoursProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HonoursProject.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddToDiaryPage : ContentPage
    {
        List<UserGoal> tempGoalList = new List<UserGoal>();
        ViewModelBase vmb = new ViewModelBase();

        public AddToDiaryPage()
        {
            //Mood mood = new Mood();
             InitializeComponent();
            CheckGoals();
        }

        private async void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            CheckBox checkbox = (CheckBox)sender;
            string name = (string)checkbox.BindingContext;
            ViewModelBase vmb = new ViewModelBase();
            List<UserGoal> goals = (List<UserGoal>)await vmb.GoalStore.GetAsync(true);
            List<UserGoal> tempGoals = (List<UserGoal>)await vmb.TempUserGoalStore.GetAllGoals();
            if (checkbox.IsChecked)
            {
                foreach (var goal in goals)
                {
                    if(goal.GoalName == name) 
                    {
                        await vmb.TempUserGoalStore.AddGoal(goal);
                        return;
                    }
                }
                UserGoal addedGoal = new UserGoal { GoalName = name };
                await vmb.TempUserGoalStore.AddGoal(addedGoal);
                return;
            }
            else if (checkbox.IsChecked == false)
            {
                foreach (var tempGoal in tempGoals)
                {
                    if(tempGoal.GoalName == name) 
                    {
                        await vmb.TempUserGoalStore.RemoveGoal(tempGoal);
                        return;
                    }
                }
            }
        }

        protected override void OnAppearing() 
        {
            base.OnAppearing();
            CheckGoals();
        }

        public async void CheckGoals() 
          {
            int id = await vmb.TempUserGoalStore.GetUserID();
            User user = await vmb.UserStore.GetAsync(id);

            if(user.UserGoals.Count == 0) 
            {
                goalsList.IsVisible = false;
                emptyGoals.IsVisible = true;
            }else 
            {
                goalsList.IsVisible = true;
                emptyGoals.IsVisible = false;
            
            }
        
        }
    }
}