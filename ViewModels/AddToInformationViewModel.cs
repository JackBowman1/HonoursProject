using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using HonoursProject.ViewModels;
using HonoursProject.Models;
using HonoursProject.Services;
using HonoursProject.Views;

namespace HonoursProject.ViewModels
{
    public class AddToInformationViewModel : ViewModelBase
    {

        private string name;
        private string description;
        private string image;
        private string descriptionImage;

        public Command AddCommand { get; }

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
        public AddToInformationViewModel() 
        {
            AddCommand = new Command(OnAddInformation);
            this.PropertyChanged += (_, __) => AddCommand.ChangeCanExecute();
        }

        private async void OnAddInformation()
        {
            List<int> informationIDs = new List<int>();
            ViewModelBase vmb = new ViewModelBase();

            foreach (var item in await vmb.DataStore.GetAsync())
            {
                informationIDs.Add(item.Id);
            }

            int generatedID = GenerateIncrementedID(informationIDs);

            InformationSection newItem = new InformationSection()
            {
                Id = generatedID,
                Name = Name,
                Description = Description,
                Image = Image,
                DescriptionSections = new List<DescriptionSection>()
            };

            await DataStore.AddAsync(newItem);
            //await Database.AddInformation(newItem);

            
            await DataStore.GetAsync();

            await Shell.Current.GoToAsync($"{nameof(AddToDescriptionSectionPage)}?{nameof(AddToDescriptionSectionViewModel.InformationSectionID)}={generatedID}");

            // This will pop the current page off the navigation stack
            //await Shell.Current.GoToAsync("..");
        }


        public int GenerateIncrementedID(List<int> ids)
        {
            int count = 0;
            foreach (var id in ids)
            {
                if (id > count)
                {
                    count = id;
                }
            }

            return count + 1;
        }

    }
}
