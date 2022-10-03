using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HonoursProject.Models
{
    public class Diary
    {
        /// <summary>
        /// Class Properties 
        /// </summary>
        
        public int DiaryID { get; set; }
        public string DiaryName { get; set; }

        /// <summary>
        /// Navigational properties
        /// </summary>

        //1 to many relationship - 1 diary can have many entries
        public List<DiaryEntry> Entries { get; set; }


        public Diary()
        {
            this.DiaryID = 1;
            this.DiaryName = "My Diary";
            this.Entries = new List<DiaryEntry>();
            
        }

    }
    
}
