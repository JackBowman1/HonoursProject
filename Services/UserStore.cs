using HonoursProject.Models;
using HonoursProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HonoursProject.Services
{
    public class UserStore : IDataStore<User>
    {
        readonly List<User> Users = new List<User>();
        readonly List<UserGoal> UserGoals = new List<UserGoal>();

        public UserStore() 
        {
            Users = InitialiseUsers();
           // UserGoals = InitialiseGoals();
        
        }

        public async Task<bool> AddAsync(User user)
        {

            Users.Add(user);

            return await Task.FromResult(true);
        }

        
        public async Task<bool> UpdateAsync(User user)
        {
            var oldInfoSection = Users.Where((User arg) => arg.UserID == user.UserID).FirstOrDefault();
            Users.Remove(oldInfoSection);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var oldUser = Users.Where((User arg) => arg.UserID == id).FirstOrDefault();

            return await Task.FromResult(true);
        }

        public async Task<User> GetAsync(int id)
        {
            return await Task.FromResult(Users.FirstOrDefault(s => s.UserID == id));
        }

        public async Task<IEnumerable<User>> GetAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(Users);
        }
     
        public List<User> InitialiseUsers() 
        { 
            
            List<User> users = new List<User>()
            {
                new User
                {
                    UserID = 1, Username = "john@john.com", Password = HashPassword("johnsPassword123"), DOB = "02/03/1998", FirstName = "John", LastName = "johnson", DiaryID = 1,
                        Diary = new Diary
                        {
                            DiaryName = "Johns Diary", DiaryID = 1,
                            Entries = new List<DiaryEntry>
                            {
                                new DiaryEntry
                                {
                                    DiaryEntryID = 1,
                                    DiaryID = 1,
                                    Title = "Johns First Diary entry",
                                    Text = "This is my first diary entry",
                                    EntryDate = DateTime.Now.ToString("dd,dddd"),
                                    Colour ="DarkBlue",
                                    Mood = new Mood("Happy"),
                                    Icon = "HappyFace.png"
                                }
                            }
                        },
                        UserGoals = new List<UserGoal>
                        {
                            new UserGoal
                            {
                                GoalName = "Goal 1",
                            },
                            new UserGoal
                            {
                                GoalName = "Goal 2",
                            },
                            new UserGoal
                            {
                                GoalName = "Goal 3",
                            },
                            new UserGoal
                            {
                                GoalName = "Goal 4",
                            }
                        }
                        
                },
                 new User
                {
                    UserID = 2, Username = "Marc@marc.com", Password = HashPassword("123") ,DOB = "09/08/1989", FirstName = "Marc", LastName = "Marcson",  DiaryID = 2,
                        Diary = new Diary
                        {
                            DiaryID = 2,
                            DiaryName = "Marcs Diary",
                            Entries = new List<DiaryEntry>
                            {
                                new DiaryEntry
                                {
                                    DiaryEntryID = 1,
                                    DiaryID = 2,
                                    Title = "Marcs first diary entry",
                                    Text = "This is my first diary entry",
                                    EntryDate = DateTime.Now.ToString("dd,dddd"),
                                    Colour = "DarkBlue",
                                    Mood = new Mood("Sad"),
                                    Icon = "SadFace.png"
                                },
                                new DiaryEntry
                                {
                                    DiaryEntryID = 2,
                                    DiaryID = 2,
                                    Title = "Marcs second diary entry",
                                    Text = "This is my second diary entry",
                                    EntryDate = DateTime.Now.ToString("dd,dddd"),
                                    Colour = "DarkGreen",
                                    Mood = new Mood("Happy"),
                                    Icon = "HappyFace.png"
                                },
                                new DiaryEntry
                                {
                                    DiaryEntryID = 3,
                                    DiaryID = 2,
                                    Title = "Marcs third diary entry",
                                    Text = "This is my third diary entry",
                                    EntryDate = DateTime.Now.ToString("dd,dddd"),
                                    Colour = "Purple",
                                    Mood = new Mood("Happy"),
                                    Icon = "HappyFace.png"
                                },
                                new DiaryEntry
                                {
                                    DiaryEntryID = 4,
                                    DiaryID = 2,
                                    Title = "Marcs fourth diary entry",
                                    Text = "This is my fourth diary entry",
                                    EntryDate = DateTime.Now.ToString("dd,dddd"),
                                    Colour = "Red",
                                    Mood = new Mood("Not Okay"),
                                    Icon = "NotHappyFace.png"
                                },
                                new DiaryEntry
                                {
                                    DiaryEntryID = 5,
                                    DiaryID = 2,
                                    Title = "Marcs fith diary entry",
                                    Text = "This is my fith diary entry",
                                    EntryDate = DateTime.Now.ToString("dd,dddd"),
                                    Colour = "DarkBlue",
                                    Mood = new Mood("Sad"),
                                    Icon = "SadFace.png"
                                }
                            }
                        }

                },
                  new User
                {
                    UserID = 3, Username = "sam@samson.com", Password = HashPassword("12367"), DOB = "02/04/1972", FirstName = "Sam", LastName = "Samson",  DiaryID = 3,
                        Diary = new Diary
                        {
                            DiaryID = 3,
                            DiaryName = "Sams Diary",
                            Entries = new List<DiaryEntry>
                            {
                                new DiaryEntry
                                {
                                    DiaryEntryID = 1,
                                    DiaryID = 3,
                                    Title = "Sams first diary entry",
                                    Text = "This is my first diary entry",
                                    EntryDate = DateTime.Now.ToString("dd,dddd"),
                                    Colour = "DarkBlue",
                                    Mood = new Mood("Sad"),
                                    Icon = "SadFace.png"
                                },
                                 new DiaryEntry
                                {
                                    DiaryEntryID = 2,
                                    DiaryID = 3,
                                    Title = "Sams first diary entry",
                                    Text = "This is my first diary entry",
                                    EntryDate = DateTime.Now.ToString("dd,dddd"),
                                    Colour = "DarkGreen",
                                    Mood = new Mood("Sad"),
                                    Icon = "SadFace.png"
                                }
                            }

                        }
                },
                new Staff
                {
                    UserID = 4, Username = "Micheal@micheal.com",Password = HashPassword("123"),  JobRole = "Employee", DOB = "12/11/2005", FirstName = "Micheal", LastName = "Michealson",  DiaryID = 4,
                        Diary = new Diary
                        {
                            DiaryID = 4,
                            DiaryName = "Micheals Diary",
                            Entries = new List<DiaryEntry>()
                        }
                },
                new Staff
                {
                    UserID = 5, Username = "Peter@peter.com",Password = HashPassword("123222"),  JobRole = "Admin", DOB = "01/08/1993", FirstName = "Pater", LastName = "Peterson",  DiaryID = 5,
                        Diary = new Diary
                        {
                            DiaryID = 5,
                            DiaryName = "Micheals Diary",
                            Entries = new List<DiaryEntry>()
                        }
                },
            };

            return users;
        
        }

        public string HashPassword(string password)
        {
            //Create the salt value using PRNG
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);

            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            string passwordHashed = Convert.ToBase64String(hashBytes);

            return passwordHashed;
        }



    }
}
