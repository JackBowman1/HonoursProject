using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace HonoursProject.Models
{
    public class DiaryEntry
    {
        public int DiaryEntryID { get; set; }
        public Mood Mood {get;set;}
        public string Title { get; set; }
        public string Text { get; set; }
        public string EntryDate { get; set; }
        public int DiaryID { get; set; }
        public string Colour { get; set; }
        public List<UserGoal> UserGoalList { get; set; }

        public string Icon { get; set; }

    }

    public class Mood
    {
        public string Name { get; set; }
        public Mood() 
        {
            Name = "name";
        
        }
        public Mood(string Name) 
        {
            this.Name = Name;
        }


        public override string ToString()
        {
            return Name;
        }
    }


}
