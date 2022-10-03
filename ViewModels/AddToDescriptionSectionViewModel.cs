using HonoursProject.Models;
using HonoursProject.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace HonoursProject.ViewModels
{
    [QueryProperty(nameof(InformationSectionID), nameof(InformationSectionID))]
    public class AddToDescriptionSectionViewModel :ViewModelBase
    {
        private string descriptionHeading;
        private string descriptionText;
        private int informationSectionID;

        public string DescriptionHeading 
        {
            get => descriptionHeading;
            set => SetProperty(ref descriptionHeading, value);
        }
        public string DescriptionText
        {
            get => descriptionText;
            set => SetProperty(ref descriptionText, value);
        }
        public int InformationSectionID 
        {
            get => informationSectionID;
            set => SetProperty(ref informationSectionID, value);
        }

        public Command AddSection { get; }

        public Command AddAndFinish { get; }
        public Command GoToHomePage { get; }

        public AddToDescriptionSectionViewModel() 
        {
            AddSection = new Command(AddhDescriptionSection);
            AddAndFinish = new Command(AddAndFinishDescriptionSection);
        }

        public async void AddhDescriptionSection()
        {
            var informationSection = await DataStore.GetAsync(InformationSectionID);
            int id = 1;
            foreach (var item in informationSection.DescriptionSections)
            {
                id = id + 1;
            }
            if ((DescriptionHeading == null) || (descriptionHeading == ""))
            {
                await App.Current.MainPage.DisplayAlert("Invalid entry", "Heading is empty", "ok");
            } else if ((DescriptionText == null) || (descriptionText == ""))
            {
                await App.Current.MainPage.DisplayAlert("Invalid entry", "Description text is empty", "ok");
                return;
            }
            else {
                DescriptionSection section = new DescriptionSection();
                section.ID = id;
                section.DescriptionHeading = DescriptionHeading;
                section.DescriptionText = DescriptionText;
                section.InformationSectionID = informationSectionID;

                informationSection.DescriptionSections.Add(section);

                DescriptionHeading = "";
                DescriptionText = "";

                await App.Current.MainPage.DisplayAlert("Added successfully", "Description section has been added", "ok");
            }
            //await Shell.Current.GoToAsync("Infp")
        }

        public async void AddAndFinishDescriptionSection() 
        {
            var informationSection = await DataStore.GetAsync(InformationSectionID);
            int id = 1;
            foreach (var item in informationSection.DescriptionSections)
            {
                id = id + 1;
            }
            if ((DescriptionHeading == null) || (DescriptionHeading == "" ))
            {
                await App.Current.MainPage.DisplayAlert("Invalid entry", "Heading is empty", "ok");
            }
            else if ((DescriptionText == null) || (descriptionText == ""))
            {
                await App.Current.MainPage.DisplayAlert("Invalid entry", "Description text is empty", "ok");
                return;
            }
            else
            {
                DescriptionSection section = new DescriptionSection();
                section.ID = id;
                section.DescriptionHeading = DescriptionHeading;
                section.DescriptionText = DescriptionText;
                section.InformationSectionID = informationSectionID;

                informationSection.DescriptionSections.Add(section);

                await Shell.Current.GoToAsync($"//{nameof(InformationPage)}");
                //await Shell.Current.GoToAsync("Infp")
            }
        }


    }
}
