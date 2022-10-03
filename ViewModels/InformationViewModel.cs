using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using HonoursProject.Views;
using Xamarin.Essentials;
using System.Text;
using Xamarin.Forms;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using HonoursProject.Models;
using SQLite;
using HonoursProject.Services;

namespace HonoursProject.ViewModels
{
    public class InformationViewModel : ViewModelBase
    {
        private InformationSection selectedInformation;

        public ObservableCollection<InformationSection> InformationSections { get; }
        public ObservableCollection<DescriptionSection> DescriptionSections { get; }
        public Command AddInformationCommand { get; }
        public Command LoadInformationCommand { get; }
        public Command goToRegisterUserPage { get; }
        public Command RefreshCommand { get; }
        public Command TextToSpeechCommand { get; }
        public Command<InformationSection> InformationSectionTapped { get; }

        public InformationSection SelectedInformationSection
        {
            get => selectedInformation;
            set
            {
                SetProperty(ref selectedInformation, value);
                onInformationSectionTapped(value);
            }
        }
        public InformationViewModel()
        {

            if (InformationSections == null)
            {
                InformationSections = new ObservableCollection<InformationSection>();
                DescriptionSections = new ObservableCollection<DescriptionSection>();

                InformationSections = InitialiseInfoSections();
            }
            //Mock Data Store
            LoadInformationCommand = new Command(async () => await ExecuteLoadInfoCommand());
     //       RefreshCommand = new Command(async () => await Refresh());
            AddInformationCommand = new Command(OnAddInformation);

            TextToSpeechCommand = new Command(OnTextToSpeechCommand);

            goToRegisterUserPage = new Command(goToTest);

            InformationSectionTapped = new Command<InformationSection>(onInformationSectionTapped);
        }

        private void OnTextToSpeechCommand()
        {
       //     var settings = new SpeechOptions()
       //     {
       //         Volume = 2.0f,
       //         Pitch = 1.0f
       //     };
            TextToSpeech.SpeakAsync("It ");
        }
        private async void OnAddInformation(object obj)
        {
            await Shell.Current.GoToAsync(nameof(AddInformationPage));
        }
        private async void goToTest(object obj)
        {
            await Shell.Current.GoToAsync(nameof(RegisterGoalsPage));
        }
        public async void onInformationSectionTapped(InformationSection informationSection)
        {
            if (informationSection == null)
            {
                return;
            }
            else
            {
                
                await Shell.Current.GoToAsync($"{nameof(InformationDetailPage)}?{nameof(InformationDetailViewModel.InformationID)}={informationSection.Id}");
            }
        }

        
        public async Task ExecuteLoadInfoCommand()
        {
            IsBusy = true;
            try
            {
                InformationSections.Clear();
                var info = await DataStore.GetAsync(true);
                foreach (var item in info)
                {
                    InformationSections.Add(item);
                }
            }

             catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing() 
        {
            IsBusy = true;
            selectedInformation = null;
        
        }




        public ObservableCollection<InformationSection> InitialiseInfoSections()
        {

            // string image = "https://themeadowglade.com/wp-content/uploads/2020/04/How-To-Cope-Coping-Mechanisms-Meadowglade.jpg";
            string image = "Coping.png";
            


            InformationSections.Add(new InformationSection
            {
                Id = 1,
                Name = "How to cope",
                Description = "Learn ways and methods to deal with loneliness.",
                Image = image,
                DescriptionSections = new List<DescriptionSection>() { new DescriptionSection {DescriptionHeading = "Coping with loneliness", DescriptionText = "Loneliness can be challenging and at times when we are going through difficult challenges it can feel as if there’s no one to turn to support. It can be hard to find ways to cope but if you are experiencing loneliness, there are some methods and things that you can do to help:" },
                                                                       new DescriptionSection {DescriptionHeading = "Acceppt that you are feeling lonely.", DescriptionText = "Loneliness can be hard to deal with and sometimes even harder to accept that you are feeling this way. What can sometimes happen is that we then try to supress these feelings and hide them which can often intensify this emotion or make it worse. It is important to realise that acknowledging that you are lonely, can then allow you to make a plan and seek help In order to deal with loneliness." }}
            });

            InformationSections.Add(new InformationSection
            {
                Id = 2,
                Name = "About loneliness and isolation",
                Description = "Read our page to find information about loneliness.",
                Image = image,
                DescriptionSections = new List<DescriptionSection>() { new DescriptionSection {DescriptionHeading = "Loneliness", DescriptionText = "We all feel loneliness from time to time. The feeling of loneliness is personal, so will vary from person to person and each of us will have different experiences of it. Loneliness is when you’re in a mental state of discomfort or distress, which can then result in a person perceiving some sort of void between one’s desire for a social interaction or connection and true experiences of it. Loneliness can hit us all, even those who are with others through most of the day or in a long-lasting marriage. Evidence suggest that loneliness can result in a serious threat to health and well-being, which is why it is important to seek help from professionals and from others. " } }
            });

            InformationSections.Add(new InformationSection
            {
                Id = 3,
                Name = "Useful Links and information",
                Description = "Other links and sources to help",
                Image = image,
                DescriptionSections = new List<DescriptionSection>() { new DescriptionSection {DescriptionHeading = "Description heading for section 1 in Useful Links", DescriptionText = "Description Text for useful links 1" },
                                                                       new DescriptionSection {DescriptionHeading = "Description heading for section 2 in Useful Link", DescriptionText = "Description Text for useful links 2" } }
            });

            //     InformationSections.Add(new InformationSection { Id = "4", Name = "This is a test", Description = "This is a test description", Image = "", DescriptionSections = new List<DescriptionSection>()  { new DescriptionSection { Image = image, DescriptionHeading = "this is a descriptionHeading1", DescriptionText = "this is a description section 1"}, 
            //                                                                                                                                                                                                         new DescriptionSection { Image = "", DescriptionHeading = "This is a description heading 2", DescriptionText = "This is a description section 2"}} } );


            return InformationSections;
        }

    }












 //   public class InformationSection 
//    {
//        [PrimaryKey,AutoIncrement]
//        public int Id { get; set; }
//        public string Name { get; set; }
//        public List<DescriptionSection> DescriptionSections { get; set; }
//        public string Description { get; set; }
//        public string Image { get; set; }

  //  }
  //  public class DescriptionSection 
   // {
        //  public string Id { get; set; }

     //   public string DescriptionHeading { get; set; }
     //   public string DescriptionText { get; set; }
     //   public string DescriptionImage { get; set; }
      //  public string Image { get; set; }        
    //}
}
