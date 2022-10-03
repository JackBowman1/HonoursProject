using HonoursProject.Models;
using HonoursProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HonoursProject.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditDetailsPage : ContentPage
    {
        EditDetailsViewModel edvm;
        ViewModelBase vmb = new ViewModelBase();
        public EditDetailsPage()
        {
            InitializeComponent();
            BindingContext = edvm = new EditDetailsViewModel();
            GoalDisplay();
        }

        protected override void OnAppearing() 
        {
            edvm.GetUser();
            GoalDisplay();
        }

        public async void GoalDisplay() 
        {
            int id = await vmb.TempUserGoalStore.GetUserID();
            User user = await vmb.UserStore.GetAsync(id);
           if(user.UserGoals.Count == 0) 
           {
               notEmptyGoalDisplay.IsVisible = false;
               emptyGoalDisplayEditDetails.IsVisible = true;
           }
            else 
            {
                notEmptyGoalDisplay.IsVisible = true;
                emptyGoalDisplayEditDetails.IsVisible = false;
           }
        }
    }
}