using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HonoursProject.Models;
using HonoursProject.ViewModels;

namespace HonoursProject.Services
{
    public class DiaryStore : IDataStore<Diary>
    {
        ViewModelBase vmb = new ViewModelBase();
        readonly List<Diary> Diarys = new List<Diary>();
        readonly List<Mood> Moods = new List<Mood>();
        public DiaryStore()
        {
            //  Diarys = InitialiseDiary();
                InitialiseDiary2();

        }

        public async Task<bool> AddAsync(Diary diary)
        {
            Diarys.Add(diary);

            return await Task.FromResult(true);
        }


        public async Task<bool> UpdateAsync(Diary diary)
        {

            var oldDiary = Diarys.Where((Diary arg) => arg.DiaryID == diary.DiaryID).FirstOrDefault();
            int index = Diarys.IndexOf(oldDiary);
            //Diarys.Remove(oldDiary);
            Diarys.Insert(index,diary);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var oldInfo = Diarys.Where((Diary arg) => arg.DiaryID == id).FirstOrDefault();

            return await Task.FromResult(true);
        }

        public async Task<Diary> GetAsync(int id)
        {
            InitialiseDiary2();
            return await Task.FromResult(Diarys.FirstOrDefault(s => s.DiaryID == id));
        }

        public async Task<IEnumerable<Diary>> GetAsync(bool forceRefresh = false)
        {
            InitialiseDiary2();
            return await Task.FromResult(Diarys);
        }
        public async Task<IEnumerable<Mood>> GetMoodsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(Moods);
        }

//        private List<Diary> InitialiseDiary()
//        {
//
//            Mood happy = new Mood("Happy");
//            // UserMood.Add(Sad);
//            // UserMood.Add(Angry);
//            // UserMood.Add(Sad);
//            Diarys.Add(
//                new Diary
//                {
//                    DiaryID = 1,
//                    DiaryName = "My happy Diary",
//                    Entries = new List<DiaryEntry>
//                    {
//                        new DiaryEntry
//                        {
//                            Title = "My first diary entry",
//                            Text = "My first diary is actually so class lolz",
//                            EntryDate = DateTime.Now,
//                            Mood = happy
//                        },
//
//                        new DiaryEntry 
//                        {
//                            Title = "My second diary entry",
//                            Text = "This is class lolz",
//                            EntryDate = DateTime.Now,
//                            Mood = happy
//                        }
//                    }
//                }
//            );
//            return Diarys;
//        }

        private async void InitialiseDiary2()
        {
            Diarys.Clear();
            var users = await vmb.UserStore.GetAsync();

            foreach (var user in users)
            {
                Diarys.Add(user.Diary);
            }
        }
    }
}
