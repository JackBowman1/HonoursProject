using HonoursProject.Models;
using HonoursProject.Services;
using HonoursProject.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace HonoursProject.ViewModels
{

    public class DiaryViewModel : ViewModelBase
    {

        private int diaryEntryID;
        private Mood mood;
        private string diaryName;
        private string text;
        private string entryDate;


        private List<DiaryEntry> diaryEntries;
        public int DiaryID
        {
            get
            {
                return diaryEntryID;
            }
            set
            {
                diaryEntryID = value;
            }
        }
        public string DiaryName
        {
            get => diaryName;
            set => SetProperty(ref diaryName, value);
        }
        public string Text
        {
            get => text;
            set => SetProperty(ref text, value);
        }
        public string EntryDate
        {
            get => entryDate;
            set => SetProperty(ref entryDate, value);
        }

        public List<DiaryEntry> DiaryEntries
        {
            get => diaryEntries;
            set => SetProperty(ref diaryEntries, value);
        }

        public Mood Mood
        {
            get => Mood;
            set => SetProperty(ref mood, value);

        }

        public string Colour { get; set; }
        public ObservableCollection<Diary> Diary { get; set; }

        public ObservableCollection<DiaryEntry> DiaryEntryDisplay { get; set; }

        public ObservableCollection<DiaryGroup> GroupedEntries { get; set; }
        public ObservableCollection<Mood> UserMood { get; set; }
        public Command AddEntryCommand { get; }
        public Command LoadDiaryCommand { get; }

        public Command DetailedEntry { get; }
        public DiaryViewModel()
        {
            try
            {
                Diary = new ObservableCollection<Diary>();
                DiaryEntryDisplay = new ObservableCollection<DiaryEntry>();
                GroupedEntries = new ObservableCollection<DiaryGroup>();
                DiaryEntries = new List<DiaryEntry>();
                LoadDiaries();

                foreach (Diary item in Diary)
                {
                    DiaryID = item.DiaryID;
                    DiaryName = item.DiaryName;
                }

                var Diary1 = Diary;

                LoadDiaryCommand = new Command(() => LoadDiaries());
                DetailedEntry = new Command(GoToDetailedEntry);

                var entry = DiaryEntries;

                AddEntryCommand = new Command(GoToAddToDiary);
            }
            catch (Exception e)
            {
                var j = e.Message;
            }
        }


        public async void GoToDetailedEntry(object parameter)
        {

            DiaryEntry entry = (DiaryEntry)parameter;

            string ids = entry.DiaryID.ToString() + " " + entry.DiaryEntryID.ToString() + " ";

            await Shell.Current.GoToAsync($"{nameof(DetailedDiaryEntryPage)}?{nameof(DetailedDiaryEntryViewModel.IDs)}={ids}");
        }

        public async void GoToAddToDiary()
        {
            Diary d = new Diary();
            foreach (var item in Diary)
            {
                d.DiaryID = item.DiaryID;
                d.DiaryName = item.DiaryName;
            }        
            if (d == null)
            {
                return;
            }
            else
            {
                await Shell.Current.GoToAsync($"{nameof(AddToDiaryPage)}?{nameof(AddToDiaryViewModel.DiaryID)}={d.DiaryID}");
            }
        }

        public async void LoadDiaries() 
        {
            IsBusy = true;
            try
            {
                Diary.Clear();
                DiaryEntryDisplay.Clear();
                GroupedEntries.Clear();

                var id = await TempUserGoalStore.GetUserID();

                var displayDiary = await DiaryStore.GetAsync(id);

                Diary.Add(displayDiary);

                foreach (var name in Diary)
                {
                    DiaryName = name.DiaryName;
                } 

                ObservableCollection<DiaryEntry> unOrderedDiaryEntry= new ObservableCollection<DiaryEntry>();

                foreach (var entry in displayDiary.Entries)
                {
                    unOrderedDiaryEntry.Add(entry);
                }

                List<DiaryEntry> entries = displayDiary.Entries;
             //   unOrderedDiaryEntry.Reverse();

                //Orders the list
                OrderList(unOrderedDiaryEntry);

           //     GroupedEntries.Add(new DiaryGroup(DateTime.Now.ToString("MMMM,yyyy"), unOrderedDiaryEntry));

                var count = DiaryEntryDisplay.Count;
            }
            catch (Exception e) 
            {
                var j = e.Message;            
            }finally
            {
                IsBusy = false;
            }
        }

        public void OrderList(IEnumerable<DiaryEntry> unOrderedDiaryEntries) 
        {
            var orderedList = unOrderedDiaryEntries.OrderByDescending(e => e.DiaryEntryID);

            DiaryEntryDisplay.Clear();



            string monthYear = DateTime.Now.ToString("MMMM, yyyy");
            foreach (var item in orderedList)
            {
                DiaryEntryDisplay.Add(item);
                //GroupedEntries.Add(new Grouping
            }


   
          //  GroupedEntries.Add(new DiaryGroup(monthYear, DiaryEntryDisplay));
            //var grouped = from Model in orderedList
              //            group Model by DateTime.Now.ToString("MM,yyyy") into Group
                //          select new Grouping<string, DiaryEntry>(Group.Key, Group);

//            GroupedEntries = new ObservableCollection<Grouping<string, DiaryEntry>>(grouped);
        }

        public void OnAppearing()
        {
            IsBusy = true;
        }




    }

    public class DiaryGroup : List<DiaryEntry>
    {
        public string Date { get; set; }
        public DiaryGroup(string date, ObservableCollection<DiaryEntry> entries) : base(entries)
        {
            Date = date;
        }

    }
}

