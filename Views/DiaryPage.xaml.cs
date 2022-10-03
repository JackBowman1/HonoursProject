using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HonoursProject.Models;
using HonoursProject.Services;
using HonoursProject.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HonoursProject.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DiaryPage : ContentPage
    {
        DiaryViewModel dVM;
        ViewModelBase vmb = new ViewModelBase();
        public DiaryPage()
        {
            try
            {
                InitializeComponent();
                BindingContext = dVM = new DiaryViewModel();
                CheckDiaryEntries();
            }
            catch (Exception e)
            {
                var j = e.Message;
            }
        }

        public async void CheckDiaryEntries() 
        {
            int id = await vmb.TempUserGoalStore.GetUserID();
            User user = await vmb.UserStore.GetAsync(id);
            try
            {
                var diaries = await vmb.DiaryStore.GetAsync();
                var diary = await vmb.DiaryStore.GetAsync(user.DiaryID);


                if (diary.Entries.Count == 0)
                {

                    EmptyDiaryDisplay.IsVisible = true;
                }
                else
                {
                    EmptyDiaryDisplay.IsVisible = false;
                }
            }
            catch (Exception e) 
            {
                var j = e.Message;
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            //DiaryPage p = new DiaryPage();
            dVM.OnAppearing();
            dVM.LoadDiaries();
            CheckDiaryEntries();
        }
    }
}