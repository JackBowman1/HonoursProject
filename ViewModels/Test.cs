using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using HonoursProject.Models;
using HonoursProject.Services;
using HonoursProject.Views;
using Xamarin.Forms;

namespace HonoursProject.ViewModels
{
    [QueryProperty(nameof(CategoryType), nameof(CategoryType))]
    public class Test : ViewModelBase
    {
        private string categoryType;

        public string CategoryType 
        {
            get => categoryType;
            set 
            { 
                SetProperty(ref categoryType, value);
                LoadDiaries(value);
            }

        }
        public ObservableCollection<UserGoal> UserGoal1 { get; set; }

        public ObservableCollection<UserGoal> UserGoalDisplay { get; set; }

        public Test()
        {
            UserGoal1 = new ObservableCollection<UserGoal>();
            UserGoalDisplay = new ObservableCollection<UserGoal>();

            //LoadDiaries();

        }

        public async void LoadDiaries(string type)
        {
            IsBusy = true;
            try
            {
                UserGoal1.Clear();
                UserGoalDisplay.Clear();

                var displayGoal = await GoalStore.GetAsync(true);

                foreach (var item in displayGoal)
                {
                    if (item.Type == type) {
                        UserGoalDisplay.Add(item);
                    }
                }

            }
            catch (Exception e)
            {
                var j = e.Message;
            }
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
        }



    }
}

