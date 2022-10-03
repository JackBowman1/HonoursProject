using HonoursProject.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace HonoursProject.ViewModels
{
    [QueryProperty(nameof(IDs), nameof(IDs))]
    public class DetailedDiaryEntryViewModel : ViewModelBase
    {
        private string ids;
        private string heading;
        private string text;
        private string colour;
        private string date;
        public string IDs 
        {
            get => ids;
            set
            {
                SetProperty(ref ids, value);
                LoadEntry(value);
            } 
        }
        public string Date 
        {
            get => date;
            set => SetProperty(ref date, value);
        }
        public string Colour 
        {
            get => colour;
            set => SetProperty(ref colour, value);
        }
        public string Heading 
        {
            get => heading;
            set => SetProperty(ref heading, value); 
        }

        public string Text
        {
            get => text;
            set => SetProperty(ref text, value);
            }
        public DetailedDiaryEntryViewModel() 
        {
          //  GetEntry();   
        }

        public async void LoadEntry(string entry) 
        {
            string[] ids = entry.Split(' ');
            List<int> intIDs = new List<int>();
            
            foreach (var id in ids)
            {
                try 
                {
                    intIDs.Add(int.Parse(id));
                }
                catch 
                {
                
                }
            }

            int diaryID = intIDs[0];
            int diaryEntryID = intIDs[1];

            Diary diary = await DiaryStore.GetAsync(diaryID);
            DiaryEntry usersEntry = new DiaryEntry();

            foreach (var diaryEntry in diary.Entries)
            {
                if(diaryEntry.DiaryEntryID == diaryEntryID) 
                {
                    usersEntry = diaryEntry;
                    Heading = usersEntry.Title;
                    Text = usersEntry.Text;
                    Date = usersEntry.EntryDate; 
                    Colour = usersEntry.Colour;
                    return;
                }
            }
        }

 
    }
}
