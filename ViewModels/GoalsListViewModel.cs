using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using HonoursProject.Models;
using HonoursProject.Services;
using HonoursProject.Views;
using Xamarin.Forms;
using Xamarin.Essentials;


namespace HonoursProject.ViewModels
{
    
    [QueryProperty(nameof(CategoryType), nameof(CategoryType))]
    public class GoalsListViewModel : ViewModelBase
    {
        private string categoryType;

        public string CategoryType
        {
            get => categoryType;
            set
            {
                SetProperty(ref categoryType, value);
                LoadGoals(value);
            }

        }
        public ObservableCollection<UserGoal> UserGoal1 { get; set; }

        public ObservableCollection<UserGoal> UserGoalDisplay { get; set; }

        public Command TextToSpeechCommand { get; }

        public GoalsListViewModel()
        {
            UserGoal1 = new ObservableCollection<UserGoal>();
            UserGoalDisplay = new ObservableCollection<UserGoal>();
            TextToSpeechCommand = new Command(OnTextToSpeech);
            //LoadDiaries();

        }

        public void OnTextToSpeech(object parameter) 
        {

            string text = (string)parameter;

            var settings = new SpeechOptions()
            {
                Volume = 1.0f,
                Pitch = 1.0f
            };

            TextToSpeech.SpeakAsync(text, settings);
        }
        public async void LoadGoals(string type)
        {
            IsBusy = true;
            try
            {
                UserGoal1.Clear();
                UserGoalDisplay.Clear();

                var displayGoal = await GoalStore.GetAsync(true);

                foreach (var item in displayGoal)
                {
                    if (item.Type == type)
                    {
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

