using HonoursProject.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace HonoursProject.ViewModels
{
    public class DiaryEntryGoalsViewModel: ViewModelBase
    {
        public Command BackButtonOvveride { get; }
        public DiaryEntryGoalsViewModel() 
        {
            BackButtonOvveride = new Command(OnBackPressedOverride);
        
        }

        public async void OnBackPressedOverride()
        {
            await Shell.Current.GoToAsync($"/{nameof(AddToDiaryPage)}");
        }
    }
}
