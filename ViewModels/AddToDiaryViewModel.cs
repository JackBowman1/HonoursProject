using HonoursProject.Models;
using HonoursProject.Services;
using HonoursProject.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HonoursProject.ViewModels
{
    [QueryProperty(nameof(DiaryID), nameof(DiaryID))]
    public class AddToDiaryViewModel : ViewModelBase
    {
        private int diaryID;
        private string diaryEntryTitle;
        private string text;
        private Mood selectedMood;
        private Mood displayMood;
        

        public int DiaryID 
        {
            get
            {
                return diaryID;
            }
            set
            {
                diaryID = value;
          //      LoadDiaryId(value);
            }
        }
        public string EntryTitle 
        {
            get => diaryEntryTitle;
            set => SetProperty(ref diaryEntryTitle, value);
        }

        public string Text
        {
            get => text;
            set => SetProperty(ref text, value);
        }

        public Mood SelectedMood
        {
            get => selectedMood;
            set => SetProperty(ref selectedMood, value);
        }

        public Mood DisplayMood
        {
            get => displayMood;
            set => SetProperty(ref displayMood, value);
        }

        public List<Mood> UserMoods { get; set; }
        public ObservableCollection<UserGoal> UserGoalList { get; set; }
        public Command AddEntryCommand { get; }
        public AddToDiaryViewModel() 
        {
            try
            {
                List<Mood> moods = InitialiseMoods();
                GetUserGoals();
                UserMoods = new List<Mood>();
                DisplayMood = new Mood();
                foreach (var mood in moods)
                {
                    UserMoods.Add(mood);
                }
                int n = DiaryID;
                AddEntryCommand = new Command(OnAddDiaryEntry);
                this.PropertyChanged += (_, __) => AddEntryCommand.ChangeCanExecute();
            }
            catch (Exception e) 
            {
                var j = e.Message;
            }

        }

        private void GoalsCheckBoxChanged(object sender, CheckedChangedEventArgs e)
        {

        }
        public async void OnAddDiaryEntry() 
        {
            try
            {
                Diary Diary = await DiaryStore.GetAsync(DiaryID);
                string moodName = SelectedMood.Name;
                Mood mood = new Mood(moodName);
                var goals = await TempUserGoalStore.GetAllGoals();
                List<UserGoal> userGoals = new List<UserGoal>();

                foreach (var goal in goals)
                {
                    userGoals.Add(goal);
                }
                await TempUserGoalStore.ClearAllGoals();

                string colour = "DarkBlue";
                if (Diary.Entries.Count !=0) 
                {
                    DiaryEntry lastEntry = Diary.Entries[Diary.Entries.Count - 1];
                    colour = GetLastEntryColour(lastEntry);

                }
                string icon = "";
                if (moodName == "Happy") 
                {
                    icon = "HappyFace.png";
                }else if(moodName == "Okay") 
                {
                    icon = "OkayFace.png";
                
                }else if(moodName == "Not Okay") 
                {
                    icon = "NotOkayFace.png";
                }else if (moodName == "Sad") 
                {
                    icon = "SadFace.png";
                }
                

                CultureInfo provider = CultureInfo.InvariantCulture;
                DiaryEntry newEntry = new DiaryEntry
                {
                    DiaryEntryID = Diary.Entries.Count + 1,
                    Text = Text,
                    DiaryID = DiaryID,
                    EntryDate = DateTime.Now.ToString("dd \n MMMM "),
                    Mood = mood,
                    Title = Title,
                    UserGoalList = userGoals,
                    Colour = colour,
                    Icon = icon
                };

                Diary.Entries.Add(newEntry);
           //     await DiaryStore.UpdateAsync(Diary);
           //     DiaryViewModel dvm = new DiaryViewModel();
           //     await Shell.Current.GoToAsync($"/{nameof(EntryGoalsPage)}");
                await Shell.Current.GoToAsync("..");
          
            }
            catch (Exception e)
            { 
                var j = e.Message;
            }
        }


        public string GetLastEntryColour(DiaryEntry lastEntry)
        {
            string  currentColour = "DarkBlue";

            if (lastEntry.Colour == "DarkBlue")
            {
                currentColour = "DarkGreen";
            }
            else if (lastEntry.Colour == "DarkGreen")
            {
                currentColour = "Purple";
            }
            

            return currentColour;
        }
        public async void GetUserGoals() 
        {
            try
            {
                int id = await TempUserGoalStore.GetUserID();
                var user = await UserStore.GetAsync(id);
                UserGoalList = new ObservableCollection<UserGoal>();

                foreach (var goal in user.UserGoals)
                {
                    UserGoalList.Add(goal);
                }
           }
            catch (Exception e) 
            {
                var j = e.Message;
            }
        
        }
        private List<Mood> InitialiseMoods()
        {
            List<Mood> m = new List<Mood>();
            Mood sad = new Mood("Sad");
            Mood okay = new Mood("Okay");
            Mood NotOkay = new Mood("Not Okay");
            Mood happy = new Mood("Happy");

            //Mood delighted = new Mood("Delighted");
            //Mood anxiety = new Mood("Anxious");
            //Mood angry = new Mood("Angry");

            m.AddRange(new List<Mood> { happy, okay, NotOkay, sad});

            return m;
        }
    }
}
