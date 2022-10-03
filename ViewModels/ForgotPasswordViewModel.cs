using HonoursProject.Models;
using HonoursProject.Views;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace HonoursProject.ViewModels
{
    public class ForgotPasswordViewModel : ViewModelBase
    {
        private string sentCode;
        private string userEmail;
        private string compareCode;
        public string UserEmail
        {
            get => userEmail;
            set => SetProperty(ref userEmail, value);
        }
        public string SentCode
        {
            get => sentCode;
            set => SetProperty(ref sentCode, value);
        }
        public string CompareCode
        {
            get => compareCode;
            set => SetProperty(ref compareCode, value);
        }
        public Command ResetPasswordCommand { get; }
        public ForgotPasswordViewModel()
        {
            sentCode = "";
            ResetPasswordCommand = new Command(ResetPassword);
        }

    
        public async void ResetPassword()
        {
            try
            {
                List<User> users = (List<User>)await UserStore.GetAsync();
                List<string> recepients = new List<string>();
                string recepient = UserEmail;
                recepients.Add(recepient);

                foreach (var user in users)
                {
                    if (user.Username == UserEmail)
                    {
                        try
                        {
                            Random rnd = new Random();
                            int newPassword = rnd.Next(9999);
                            string stringPassword = newPassword.ToString();
                            SentCode = stringPassword;
                            string bodyMessage = "Hello " + user.FirstName + "\n \n We have been requested to reset you password \n \n If you did not initiate this request please contact the admin: admin@admin.com. otheriwse enter your new password and you can on your way! \n \n  Your new password is:  " + stringPassword;

                            var emailMessage = new EmailMessage
                            {
                                Subject = "Reset password",
                                To = recepients,
                                Body = bodyMessage,

                            };


                            await Email.ComposeAsync(emailMessage);

                            compare(newPassword);
                            return;

                        }
                        catch (FeatureNotSupportedException fbsEx)
                        {

                        }
                    }
                }
                await App.Current.MainPage.DisplayAlert("No email found:", "We could not find an email adress. Make sure you entered the correct address if you have already registered.", "ok");
            }
            catch
            {

            }
        }
        public async void compare(int sentCode)
        {
            try
            {
                string result = "";
                while (result.Equals(""))
                {
                    result = await App.Current.MainPage.DisplayPromptAsync("Code sent successfully:", "We have sent you the reset code, please check your email and enter the code:");
                    if(result == null) 
                    {
                        return;
                    }
                }
                if (!string.IsNullOrEmpty(result))
                {

                    try
                    {
                        int parseResult = int.Parse(result);
                        if (parseResult == sentCode)
                        {
                            await Shell.Current.GoToAsync($"{nameof(AddNewPasswordPage)}?{nameof(AddNewPasswordViewModel.Email)}={UserEmail}");
                        }
                        else
                        {
                            await App.Current.MainPage.DisplayAlert("Incorrect code entered:", "The code you entered did not match", "ok");

                        }

                    }
                    catch (Exception e)
                    {
                        await App.Current.MainPage.DisplayAlert("Incorrect code entered:", "The code you entered did not match", "ok");

                    }

                }
            }
            catch(Exception e) 
            {
                var j = e.Message;
            }
        }
    }
}
