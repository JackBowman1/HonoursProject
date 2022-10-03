using System;
using System.Collections.Generic;
using System.Text;

namespace HonoursProject.Models
{
    public class User 
    {

        public int UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string DOB { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        /// <summary>
        /// Navigational properties
        /// </summary>

        //1 to 1 - 1 user can have 1 diary
        public int DiaryID { get; set; }
        public Diary Diary { get; set; }

        public List<UserGoal> UserGoals { get; set; }
        public User() 
        {
            this.Username = "jack@jack.com";
            this.Password = "123";
            this.DOB = "01/03/98";
            this.FirstName = "Jack";
            this.LastName = "Bowman";
            this.UserGoals = new List<UserGoal>();
        }

        public User(string usernameIn, string passwordIn, string DOBIn, string firstNameIn, string lastNameIn, List<UserGoal> userGoals) 
        {
            this.Username = usernameIn;
            this.Password = passwordIn;
            this.DOB = DOBIn;
            this.FirstName = firstNameIn;
            this.LastName = lastNameIn;
            this.UserGoals = userGoals;
        }



    }
}
