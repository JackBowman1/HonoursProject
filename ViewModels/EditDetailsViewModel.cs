using HonoursProject.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace HonoursProject.ViewModels
{
    public class EditDetailsViewModel : ViewModelBase
    {
        public int Email { get; set; }
        private string firstName;
        private string lastName;
        private string newPassword;

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

        public string NewPassword
        {
            get => newPassword;
            set => SetProperty(ref newPassword, value);

        }

        public Command ChangePassword { get; }
        public List<UserGoal> CurrentGoals { get; set; }
        public Command UpdateUser { get; }
        public EditDetailsViewModel()
        {
            GetUser();
            UpdateUser = new Command(GetUpdateUser);
            ChangePassword = new Command(OnPasswordChange);
        }

        public async void OnPasswordChange()
        {
            string passwordResult = await App.Current.MainPage.DisplayPromptAsync("Enter password:", "Please enter your current password");
            if(passwordResult == "" || passwordResult == null) 
            {
                await App.Current.MainPage.DisplayAlert("Invalid Password", "The password you entered is empty","ok");
            }else 
            {
                int id = await TempUserGoalStore.GetUserID();
                User user = await UserStore.GetAsync(id);
                LoginViewModel lvm = new LoginViewModel();
                if (lvm.VerifyHashPassword(passwordResult, user.Password)) 
                {
                    string newPasswordResult = await App.Current.MainPage.DisplayPromptAsync("Enter new Password:", "Please enter your new password");
                    if (newPasswordResult == null || newPasswordResult == "")
                    {
                        await App.Current.MainPage.DisplayAlert("Invalid Password", "The password you entered is empty", "ok");

                    }
                    else
                    {
                        RegisterViewModel rgvm = new RegisterViewModel();
                        user.Password = rgvm.HashPassword(newPasswordResult);
                        await App.Current.MainPage.DisplayAlert("Password changed", "Your password has successfully changed", "ok");

                    }
                }
                else 
                {
                    await App.Current.MainPage.DisplayAlert("Incorrect Password", "The password you entered is not the same", "ok");

                }
            }
        }
        public async void GetUser() 
        {
            int id = await TempUserGoalStore.GetUserID();

            var user = await UserStore.GetAsync(id);

            FirstName = user.FirstName;
            LastName = user.LastName;
            CurrentGoals = user.UserGoals;
        }

        public async void GetUpdateUser() 
        {
            int id = await TempUserGoalStore.GetUserID();
            User user = await UserStore.GetAsync(id);

            user.FirstName = FirstName;
            user.LastName = lastName;
            user.Password = NewPassword;
            string newDiaryName = FirstName + "s Diary";
            user.Diary.DiaryName = newDiaryName; 

        }

        public async void tryt() 
        {
            int id = await TempUserGoalStore.GetUserID();
            User user = await UserStore.GetAsync(id);
        }
        
    }
}
