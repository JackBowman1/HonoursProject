using HonoursProject.Models;
using HonoursProject.Views;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Xamarin.Forms;

namespace HonoursProject.ViewModels
{
    public class RegisterViewModel : ViewModelBase
    {
        private string email;
        private string firstName;
        private string lastName;
        private int age;
        private string password;
        private string confirmPassword;
        private DateTime dateOfBirth;
        public string Email 
        {
            get => email;
            set => SetProperty(ref email, value);
        }
        public string FirstName
        {
            get => firstName;
            set => SetProperty(ref firstName, value);
        }
        public string LastName
        {
            get => lastName;
            set => SetProperty(ref lastName, value);
        }
        public int Age 
        {
            get => age;
            set => SetProperty(ref age, value);
        }
        public DateTime DateOfbirth 
        {
            get => dateOfBirth;
            set => SetProperty(ref dateOfBirth, value); 
        }
        public string Password
        {
            get => password;
            set => SetProperty(ref password, value);
        }
        public string ConfirmPassword
        {
            get => confirmPassword;
            set => SetProperty(ref confirmPassword, value);
        }
        public Command OnRegister { get; }

        public RegisterViewModel() 
        {

            OnRegister = new Command(OnRegisterUser);
        
        }


        private async void OnRegisterUser() 
        {
            List<string> allFields = new List<string>();
            
            if (Email == null) 
            {
                await App.Current.MainPage.DisplayAlert("Empty email:", "Please enter your email to register", "Ok");
            }
            else if (firstName  == null) 
            {
                await App.Current.MainPage.DisplayAlert("Empty First Name:", "Please enter your first name to register", "Ok");

            }
            else if(lastName == null)
            {
                await App.Current.MainPage.DisplayAlert("Empty last name:", "Please enter your last name to register", "Ok");

            }
            else if (false) 
            {
                await App.Current.MainPage.DisplayAlert("Invalid age:", "Please enter age to register", "Ok");
            }
            else if (password == null)
            {
                await App.Current.MainPage.DisplayAlert("Empty password:", "Please enter your password to register", "Ok");

            }else if(confirmPassword == null)
            {
                await App.Current.MainPage.DisplayAlert("Empty password:", "Please confirm your password to register", "Ok");
            }else if(Password != ConfirmPassword) 
            {
                await App.Current.MainPage.DisplayAlert("Passwords do not match:", "The 2 passwords you entered do not match", "Ok");
            }else if(Password.Length < 5) 
            {
                await App.Current.MainPage.DisplayAlert("Invalid password:", "The password entered must have at least 5 characters", "Ok");
            }
            else
            {
                var users = await UserStore.GetAsync();
                List<int> diaryIDs = new List<int>();
                List<int> userIDs = new List<int>();

                foreach (var user in users)
                {
                    userIDs.Add(user.UserID);
                }
                int generatedUserID = GenerateIncrementedID(userIDs);
                foreach (var user in users)
                {
                    diaryIDs.Add(user.DiaryID);
                }
                int generatedDiaryID = GenerateIncrementedID(diaryIDs);
                User newUser = new User()
                {
                    UserID = generatedUserID,
                    Username = Email,
                    FirstName = FirstName,
                    LastName = LastName,
                    DOB = DateOfbirth.ToString("dd,MM,yyyy"),
                    Diary = new Diary {DiaryName = FirstName + "s Diary",DiaryID = generatedDiaryID },
                    DiaryID = generatedDiaryID,
                    Password = HashPassword(Password)

                };
                 await UserStore.AddAsync(newUser);

                
                var diaries = await DiaryStore.GetAsync();
                
                await Shell.Current.GoToAsync($"/{nameof(RegisterGoalsPage)}?{nameof(RegisterGoalsViewModel.UserID)}={newUser.UserID}");
             //   await new NavigationPage($"/{nameof(RegisterGoalsPage)}?{nameof(RegisterGoalsViewModel.UserID)}={newUser.UserID}")
               // await Shell.Current.GoToAsync(nameof(RegisterGoalsPage));
            }
        }

        public int GenerateIncrementedID(List<int> ids) 
        {
            int count = 0;
            foreach (var id in ids)
            {
                if (id > count)
                {
                    count = id;
                }
            }
            
            return count+1;   
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

