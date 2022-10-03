 using HonoursProject.Models;
using HonoursProject.Services;
using HonoursProject.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;


namespace HonoursProject.ViewModels
{
    [QueryProperty(nameof(InformationID), nameof(InformationID))]
    public class InformationDetailViewModel :ViewModelBase
    {
        private int informationId;
        private string name;
        private string description;          
        //private string largeDescription;
        private string image;
        private string descriptionImage;
        private ObservableCollection<DescriptionSection> descriptionSections;
        public Command EditInformation { get; }
        public Command DeleteInformation { get; }
        public Command RefreshInformation { get; }
        public Command AddNewSection { get; }

        public int Id { get; set; }
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }
        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }
        public string Image
        {
            get => image;
            set => SetProperty(ref image, value);
        }
        public string DescriptionImage
        {
            get => descriptionImage;
            set => SetProperty(ref descriptionImage, value);
        }
        public ObservableCollection<DescriptionSection> DescriptionSections
        {
            get => descriptionSections;
            set => SetProperty(ref descriptionSections, value);
        }
        public int InformationID
        {
            get
            {
                return informationId;
            }
            set
            {
                informationId = value;
                LoadInformationId(value);
            }
        }
        public InformationDetailViewModel() 
        {
            DescriptionSections = new ObservableCollection<DescriptionSection>();
            RefreshInformation = new Command(OnRefreshInformation);
            EditInformation = new Command(OnEditSection);
            DeleteInformation = new Command(OnDeleteSection);
            AddNewSection = new Command(OnAddNewSection);
        }

        public async void OnEditSection(object paramter) 
        {
            int descriptionID = (int)paramter;
            int infoID = InformationID;
            try
            {
                var informationSection = await DataStore.GetAsync(infoID);
                DescriptionSection descriptionSection = informationSection.DescriptionSections.Find(X => X.ID == descriptionID);
                string resultHeading = await App.Current.MainPage.DisplayPromptAsync("Edit Heading:", "Please enter the text you want to change to");
                if(resultHeading != null) 
                {
                    descriptionSection.DescriptionHeading = resultHeading;
                }
                string resultText = await App.Current.MainPage.DisplayPromptAsync("Edit text:", "Please enter the text you want to change to");
                if(resultText != null) 
                {
                    descriptionSection.DescriptionText = resultText;   
                }
                OnRefreshInformation();
            }
            catch 
            {
                
            }
        }
        public async void OnAddNewSection() 
        {
            await Shell.Current.GoToAsync($"{nameof(AddToDescriptionSectionPage)}?{nameof(AddToDescriptionSectionViewModel.InformationSectionID)}={InformationID}");

        }

        public async void OnDeleteSection(object parameter)
        {
            int descriptionID = (int)parameter;
            int infoID = InformationID;
            try
            {
                var informationSection = await DataStore.GetAsync(infoID);
                List<DescriptionSection> descriptionSections = informationSection.DescriptionSections;

                foreach (var section in descriptionSections)
                {
                    if (section.ID == descriptionID)
                    {
                        descriptionSections.Remove(section);
                        OnRefreshInformation();
                        return;
                    }
                }

            }
            catch(Exception e) 
            {
                var j = e.Message;
            }

            OnRefreshInformation();
        }



      


        public async void LoadInformationId(int informationId)
        {
           IsBusy = true;
           try
            {    
                var information = await DataStore.GetAsync(informationId);
                Id = information.Id;
                Name = information.Name;
                Image = information.Image;
                Description = information.Description;
                DescriptionSections.Clear();
                foreach (var section in information.DescriptionSections)
                {
                    DescriptionSections.Add(section);
                }
                //DescriptionHeading = information.DescriptionHeading;
                //LargeDescription = information.LargeDescription;
            }
            catch (Exception e)
            {
                var mess = e.Message;
                Console.WriteLine(mess);
                Debug.WriteLine("Failed to load info ");
            }
            finally 
            {
                IsBusy = false;
            }
        }

        public async void OnRefreshInformation()
        {
            IsBusy = true;
            try
            {
                DescriptionSections.Clear();

                var informationSection = await DataStore.GetAsync(informationId);
                foreach (var section in informationSection.DescriptionSections)
                {
                    DescriptionSections.Add(section);
                }

            }
            catch 
            {
                Console.WriteLine("Failed to refresh information");
            }
            finally 
            {
                IsBusy = false;
            }
        }
    }
}
