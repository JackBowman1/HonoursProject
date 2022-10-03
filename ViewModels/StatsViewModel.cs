
using HonoursProject.Models;
using Microcharts;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Entry = Microcharts.ChartEntry;

namespace HonoursProject.ViewModels
{
    public class StatsViewModel : ViewModelBase
    {
        private int totalEntries;
        private int totalGoals;
        private int happyCount;
        private int okayCount;
        private int notOkayCount;
        private int sadCount;
        private LineChart linechart;
        private PieChart pieChart;
        private RadialGaugeChart radialGaugeChart;
        private PointChart pointChart;
        private string message;

        public LineChart StatsLineChart 
        {
            get => linechart;
            set => SetProperty(ref linechart, value);
        }
        public PieChart StatsPieChart
        {
            get => pieChart;
            set => SetProperty(ref pieChart, value);
        }
    
        public RadialGaugeChart RadialGaugeChart
        {
            get => radialGaugeChart;
            set => SetProperty(ref radialGaugeChart, value);
        }

        public PointChart PointChart 
        {
            get => pointChart;
            set => SetProperty(ref pointChart, value);
        }

        public int TotalEntries 
        {
            get => totalEntries;
            set => SetProperty(ref totalEntries, value); 
        }
        public int TotalGoals
        {
            get => totalGoals;
            set => SetProperty(ref totalGoals, value);
        }
        public int HappyCount
        {
            get => happyCount;
            set => SetProperty(ref happyCount, value);
        }
        public int OkayCount 
        {
            get => okayCount;
            set => SetProperty(ref okayCount, value); 
        }
        public int NotOkayCount 
        {
            get => notOkayCount;
            set => SetProperty(ref notOkayCount, value);
        }
        public int SadCount 
        {
            get => sadCount;
            set => SetProperty(ref sadCount, value); 
        }
        public string Message 
        {
            get => message;
            set => SetProperty(ref message, value); 
        }


        public Command LoadDiaryCommand { get; }
        public StatsViewModel()
        {
            GetDiary();
            GetLineChart();
            GetPieChart();
            GetSummary();
            LoadDiaryCommand = new Command(() => GetDiary());
        }


       
        public List<Entry> GetLineChartEntries() 
        {

            List<Entry> entries = new List<Entry>
             {
                 new Entry(4)
                 {
                    Color = SkiaSharp.SKColor.Parse("#00ffaf"),
                    Label ="Happy",
                },
                new Entry(2)
                {
                    Color = SkiaSharp.SKColor.Parse("#c6ff00"),
                    Label ="Not Okay",
                },
                new Entry(3)
                {
                    Color = SkiaSharp.SKColor.Parse("#00fffd"),
                    Label ="Okay",
                },
                new Entry(1)
                {
                    Color = SkiaSharp.SKColor.Parse("#ff0000"),
                    Label ="Sad"
                }
            };

            return entries;
        }
        public async void GetPieChart()
        {

            int userId = await TempUserGoalStore.GetUserID();
            var diary = await DiaryStore.GetAsync(userId);
            List<DiaryEntry> userEntrys = diary.Entries;
            List<Entry> chartEntries = new List<Entry>();

           // string hexColour = "";
            int happyScore = 0;
            int okayScore = 0;
            int notOkayScore = 0;
            int sadScore = 0;

            foreach (var diaryEntry in userEntrys)
            {
                if (diaryEntry.Mood.Name.Equals("Happy"))
                {
                    happyScore++;
                }
                else if (diaryEntry.Mood.Name.Equals("Okay"))
                {
                    okayScore++;
                }
                else if (diaryEntry.Mood.Name.Equals("Not Okay"))
                {
                    notOkayScore++;
                }
                else
                {
                    sadScore++;
                }
            }

            Entry happyEntry = new Entry(happyScore) { Label = "Happy", TextColor = SkiaSharp.SKColor.Parse("#1ad42c"),  Color = SkiaSharp.SKColor.Parse("#0aff2f"), ValueLabelColor = SkiaSharp.SKColor.Parse("#1ad42c") };
            Entry okayEntry = new Entry(okayScore) { Label = "Okay", TextColor = SkiaSharp.SKColor.Parse("#9c9c16"), Color = SkiaSharp.SKColor.Parse("#ffff00"), ValueLabelColor = SkiaSharp.SKColor.Parse("#9c9c16") };
            Entry notOkayEntry = new Entry(notOkayScore) { Label = "Not Okay", TextColor = SkiaSharp.SKColor.Parse("#ff7400"), Color = SkiaSharp.SKColor.Parse("#ff7400"), ValueLabelColor = SkiaSharp.SKColor.Parse("#ff7400") };
            Entry sadEntry = new Entry(sadScore) { Label = "Sad", TextColor = SkiaSharp.SKColor.Parse("#ff0000"), Color = SkiaSharp.SKColor.Parse("#ff0000"), ValueLabelColor = SkiaSharp.SKColor.Parse("#ff0000") };
            chartEntries.Add(happyEntry);
            chartEntries.Add(okayEntry);
            chartEntries.Add(notOkayEntry);
            chartEntries.Add(sadEntry);

            try
            {
                StatsPieChart = new PieChart
                {
                    Entries = chartEntries,
                    LabelTextSize = 40,
                    LabelColor = SkiaSharp.SKColor.Parse("#2b00ff")
                };
            }

            catch
            {
                Console.WriteLine("Error initialising chart");
            }
        }
        public async void GetLineChart()
        {
            int userId = await TempUserGoalStore.GetUserID();
            var diary = await DiaryStore.GetAsync(userId);
            List<DiaryEntry> userEntrys = diary.Entries;
            List<Entry> chartEntries = new List<Entry>(); 

            int score = 0;
            string hexColour = "";
            foreach (var diaryEntry in userEntrys)
            {
                if (diaryEntry.Mood.Name.Equals("Happy"))
                {
                    hexColour = "#00ffaf";
                    score = 4;

                }
                else if (diaryEntry.Mood.Name.Equals("Okay"))
                {
                    hexColour = "#00fffd";
                    score = 3;
                }
                else if (diaryEntry.Mood.Name.Equals("Not Okay"))
                {
                    hexColour = "#c6ff00";
                    score = 2;
                }
                else
                {
                    hexColour = "#ff0000";
                    score = 1;
                }
                Entry entry = new Entry(score) { Color = SkiaSharp.SKColor.Parse(hexColour)};
                chartEntries.Add(entry);
            }
            try
            {
                StatsLineChart = new LineChart
                {
                    Entries = chartEntries,
                    LabelTextSize = 10,
                    LineMode = LineMode.Straight,
                    LineSize = 8,
                    PointMode = PointMode.Square,
                    PointSize = 18,
                };
            }
            
            catch
            {
                Console.WriteLine("Error initialising chart");
            }
        }

        public async void GetSummary() 
        {

            int totalMoodCount = HappyCount + OkayCount + NotOkayCount + SadCount;
            decimal happyPercentage = 0.0m;
            decimal okayPercentage = 0.0m;
            decimal notOkayPercentage = 0.0m;
            decimal sadPercentage = 0.0m;

            if (totalMoodCount != 0)
            {
                happyPercentage += Math.Round(decimal.Divide(HappyCount, totalMoodCount) *100,2);
                okayPercentage +=  Math.Round(decimal.Divide(okayCount,totalMoodCount) * 100,2);
                notOkayPercentage += Math.Round(decimal.Divide(notOkayCount, totalMoodCount) * 100, 2);
                sadPercentage += Math.Round(decimal.Divide(SadCount, totalMoodCount) * 100, 2);
            }

            string moodMessage = "";
            int id = await TempUserGoalStore.GetUserID();
            var user = await UserStore.GetAsync(id);
            Diary diary = await DiaryStore.GetAsync(user.DiaryID);

            if (diary.Entries.Count == 1)
            {
                foreach (var entry in diary.Entries)
                {
                    if (entry.Mood.Name.Equals("Happy"))
                    {
                        moodMessage = "You have started really, keep progressing";
                    }
                    else if (entry.Mood.Name.Equals("Okay"))
                    {
                        moodMessage = "You have started out well keep going";
                    }
                    else if (entry.Mood.Name.Equals("Not Okay"))
                    {
                        moodMessage = "You havent had the best of starts, but keep going you'll get there.";
                    }
                    else
                    {
                        moodMessage = "We see you have had a rough start but stay strong you'll get through this.";
                    }
                }
                Message = moodMessage;
            }
            else
            {

                if (HappyCount > OkayCount && HappyCount > NotOkayCount && HappyCount > SadCount)
                {
                    moodMessage = "You have been mostly happy whilst using the app with a percentage of: " + happyPercentage.ToString() + "%" + "\n Keep progressing you are doing great!";
                }
                else if (OkayCount > HappyCount && OkayCount > NotOkayCount && OkayCount > SadCount)
                {
                    moodMessage = "You have been mostly okay whilst using the app with a percentage of: " + sadPercentage.ToString() + "%" + "\n You are doing well keep progressing.";
                }
                else if (NotOkayCount > HappyCount && NotOkayCount > OkayCount && NotOkayCount > SadCount)
                {
                    moodMessage = "You have been mostly Not okay whilst using the app with a percentage of: " + notOkayPercentage.ToString() + "%" + "Keep trying to stay positive. Try some of our methods on the how to cope page for loneliness.";
                }
                else if (SadCount > HappyCount && SadCount > OkayCount && SadCount > NotOkayCount)
                {
                    moodMessage = "You have been mostly sad whilst using the app with a percantage of: " + sadPercentage.ToString() + "%" + "Stay strong, perhaphs try visiting some of the websites we recomend in our useful sites section.";
                }
                else
                {
                    moodMessage = "You haven't had a most prevelant mood whilst using the app. Keep using the app so we can collect more information.";
                }

                Message = moodMessage;
            }

        }

        public string GetHighestMoodMessage(double happyPercentage, double okayPercentage, double notOkayPercentage, double sadPercentage) 
        {
            string moodMessage;
            if (HappyCount > OkayCount)
            {
                if (HappyCount > NotOkayCount)
                {
                    if (HappyCount > SadCount)
                    {
                        moodMessage = "You have been mostly happy whilst using the app, with a percentage of %" + happyPercentage.ToString();
                    }
                    else
                    {
                        moodMessage = "You have been mostly Sad whilst using the app, with a percentage of %" + sadPercentage.ToString();
                    }
                }
                else
                {
                    //NOT HAPPY AND NOT OKAY COUNT
                    if (NotOkayCount > SadCount)
                    {
                        moodMessage = "You have been mostly not okay whilst using the app, with a percentage of %" + notOkayPercentage.ToString();
                    }
                    else
                    {
                        moodMessage = "You have been mostly sad whilst using the app, with a percentage of %" + sadPercentage.ToString();
                    }
                }
            }
            else
            {
                //NOT HAPPY
                if (OkayCount > NotOkayCount)
                {
                    if (OkayCount > SadCount)
                    {
                        moodMessage = "You have been mostly okay whilst using the app, with a percentage of %" + okayPercentage.ToString();
                    }
                    else
                    {
                        moodMessage = "You have been mostly sad whilst using the app, with a percentage of %" + sadPercentage.ToString();
                    }
                }
                else
                {
                    if (NotOkayCount > SadCount)
                    {
                        moodMessage = "You have been mostly not okay whilst using the app, with a percentage of %" + notOkayPercentage.ToString();
                    }
                    else
                    {
                        moodMessage = "You have been mostly sad whilst using the app, with a percentage of %" + sadPercentage.ToString();
                    }
                }
            }

            return moodMessage;
        }

        public async void GetDiary()
        {
            try
            {
                TotalEntries = 0;
                HappyCount = 0;
                OkayCount = 0;
                NotOkayCount = 0;
                SadCount = 0;
                int userId = await TempUserGoalStore.GetUserID();
                var diary = await DiaryStore.GetAsync(userId);
                List<DiaryEntry> userEntrys = diary.Entries;

                foreach (var entry in userEntrys)
                {
                    TotalEntries++;
                    if (entry.Mood.Name == "Happy")
                    {
                        HappyCount++;
                    }
                    else if (entry.Mood.Name == "Okay")
                    {
                        OkayCount++;
                    } else if (entry.Mood.Name == "Not Okay")
                    {
                        NotOkayCount++;
                    } else 
                    {
                        SadCount++;
                    }
                }
                TotalGoals = 0;
                foreach (var entry in userEntrys)
                {
                    foreach (var goal in entry.UserGoalList)
                    {
                        TotalGoals++;
                    }
                }
            //    GetPieChart();
            //    GetLineChart();
            //    GetSummary(); 
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally 
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
