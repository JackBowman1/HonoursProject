using HonoursProject.Views;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HonoursProject.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private string username;
        private string password;
        public string Username 
        {
            get => username;
            set => SetProperty(ref username, value);
        }
        public string Password
        {
            get => password;
            set => SetProperty(ref password, value);
        }

        public Command LoginCommand { get; }
        public Command RegisterCommand { get;}
        public Command ForgetPasswordCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new Command(OnLoginClicked);
            RegisterCommand = new Command(OnRegisterClicked);
            ForgetPasswordCommand = new Command(GoToForgetPassword);

        }

        


        private async void OnRegisterClicked(object obj)
        {

            await Shell.Current.GoToAsync(nameof(RegisterPage));
            
        }

        private async void OnLoginClicked(object obj)
        {
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one

            string username = Username;
            string password = Password;


            if (username == null)
            {
                await App.Current.MainPage.DisplayAlert("Empty username:", "Please enter your username to login", "Ok");
            }
            if (password == null)
            {
                await App.Current.MainPage.DisplayAlert("Empty password:", "Please enter your password to login", "Ok");
            }
            else
            {
                var users = await UserStore.GetAsync();
                

                foreach (var user in users)
                {
                    if ((user.Username == username) && VerifyHashPassword(password, user.Password))
                    {
                        await TempUserGoalStore.AddUserID(user.UserID);
                        
                         await Shell.Current.GoToAsync($"//{nameof(InformationPage)}");
                        
                        return;
                    }
                }

                await App.Current.MainPage.DisplayAlert("Invalid Login Details:", "Sorry, either your password or email is incorrect.", "Ok");
            }
        }

        public async void GoToForgetPassword()
        {
            await Shell.Current.GoToAsync($"/{nameof(ForgotPasswordPage)}");

        }
        public bool VerifyHashPassword(string password,string hashPassword)
        {
            // Get hash bytes
            byte[] passwordHashBytes = Convert.FromBase64String(hashPassword);

            // Get salt
            var salt = new byte[16];
            Array.Copy(passwordHashBytes, 0, salt, 0, 16);

            // Create hash with given salt
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);

            // Get result
            for (var i = 0; i < 20; i++)
            {
                if (passwordHashBytes[i + 16] != hash[i])
                {
                     return false;
                }
            }
            return true;

        }
    }
}
