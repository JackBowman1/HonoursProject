using HonoursProject.Models;
using HonoursProject.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace HonoursProject.ViewModels
{
    [QueryProperty(nameof(Email), nameof(Email))]
    public class AddNewPasswordViewModel: ViewModelBase
    {
        private string email;
        private string newPassword;
        public string Email 
        {
            get => email;
            set => SetProperty(ref email, value); 
        }
        public string NewPassword 
        {
            get => newPassword;
            set => SetProperty(ref newPassword, value); 
        }
        public Command ChangePasswordCommand { get; }
        public AddNewPasswordViewModel() 
        {
            ChangePasswordCommand = new Command(OnChangePassword);
        }

        public async void OnChangePassword() 
        {
            string enteredPassword = NewPassword;

            if(enteredPassword == null || enteredPassword.Equals("")) 
            {
            
            }else 
            {
                List<User> users =  (List<User>)await UserStore.GetAsync();

                User user = users.Find(x => x.Username == Email);

                RegisterViewModel rvm = new RegisterViewModel();
                user.Password = rvm.HashPassword(newPassword);

                await App.Current.MainPage.DisplayAlert("Password changed:", "Your password is now change","ok");

                await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");

            }

        }

    }
}
