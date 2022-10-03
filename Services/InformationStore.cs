using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using HonoursProject.ViewModels;
using System.Linq;
using HonoursProject.Models;
using System.Runtime.InteropServices;

namespace HonoursProject.Services
{
    public class InformationStore : IDataStore<InformationSection>
    {
       readonly List<InformationSection> InformationSections = new List<InformationSection>();
       //public InformationViewModel infoSecModel = new InformationViewModel();
       //public InformationDetailViewModel informationDetailViewModel = new InformationDetailViewModel();

        public InformationStore() 
        {
            try 
            {
                InformationSections = InitialiseInfoSections();
            } catch (Exception e) 
            { 
                var s = e.Message; 
            }
            //InformationSections = InitialiseInfoSections();
        }
        
        
        public async Task<bool> AddAsync(InformationSection info)
        {

            InformationSections.Add(info);

            return await Task.FromResult(true);
        }


        public async Task<bool> UpdateAsync(InformationSection info)
        {
            var oldInfoSection = InformationSections.Where((InformationSection arg) => arg.Id == info.Id).FirstOrDefault();
            InformationSections.Remove(oldInfoSection);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var oldInfo = InformationSections.Where((InformationSection arg) => arg.Id == id).FirstOrDefault();

            return await Task.FromResult(true);
        }

        public async Task<InformationSection> GetAsync(int id)
        {
            return await Task.FromResult(InformationSections.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<InformationSection>> GetAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(InformationSections);
        }

        public List<InformationSection> InitialiseInfoSections()
        {
            string image = "Coping.jpg";
        
            InformationSections.Add(new InformationSection
            {
                Id = 1,
                Name = "How to cope",
                Description = "Learn ways and methods to deal with loneliness.",
                Image = image,
                DescriptionSections = new List<DescriptionSection>()
                {
                    new DescriptionSection 
                    {
                        ID =1,
                        DescriptionHeading = "Coping with loneliness",
                        DescriptionText = "Loneliness can be challenging and at times when we are going through difficult challenges it can feel as if there’s no one to turn to support. It can be hard to find ways to cope but if you are experiencing loneliness, there are some methods and things that you can do to help:",
                        InformationSectionID = 1
                    },
                    new DescriptionSection 
                    {
                        ID = 2,
                        DescriptionHeading = "1. Accept that you are feeling lonely.", DescriptionText = "Loneliness can be hard to deal with and sometimes even harder to accept that you are feeling this way. What can sometimes happen is that we then try to supress these feelings and hide them which can often intensify this emotion or make it worse. It is important to realise that acknowledging that you are lonely, can then allow you to make a plan and seek help In order to deal with loneliness.",
                        InformationSectionID = 1
                    },
                    new DescriptionSection 
                    {
                        ID = 3,
                        DescriptionHeading = "2. Join an existing support group online",
                        DescriptionText = "Loneliness is now an issue that can be found widepsread accross the globe, and as a result this has allowed there to be lots of people in a similar position looking for help. Joining an online griup can help you fine people with the similar hobbies and interests and thus create new bonds. ", 
                        InformationSectionID = 1
                    },
                    new DescriptionSection 
                    {
                        ID = 4,
                        DescriptionHeading = "3. Adopt a pet", 
                        DescriptionText = "Pets such as dogs and cats can provide so many benefits and helping people to cope with loneliness is one. Not only are pets a great way to create a companionship between you and your animal, it can also help you bond with other people-walking your dog could allow you to meet other dog walkers.",
                        InformationSectionID = 1
                    },
                    new DescriptionSection
                    {
                        ID = 5,
                        DescriptionHeading = "4. Do the things you enjoy most", 
                        DescriptionText = "Filling your day with more activities and things that make you happy can allow you to stop focusing on negative emotions and improve your overall well being. \n Things like spending more times outdoors on a nice day, listening to music or simply enjoying the green space are just some of the ways to occupy your mind and boost your mood.",
                        InformationSectionID = 1
                    }
                }
            });
            InformationSections.Add(new InformationSection
            {
                Id = 2,
                Name = "About loneliness and isolation",
                Description = "Read our page to find information about loneliness.",
                Image = image,
                DescriptionSections = new List<DescriptionSection>() 
                { 
                    new DescriptionSection 
                    {
                        ID = 1,
                        DescriptionHeading = "Loneliness", 
                        DescriptionText = "We all feel loneliness from time to time. The feeling of loneliness is personal, so will vary from person to person and each of us will have different experiences of it. Loneliness is when you’re in a mental state of discomfort or distress, which can then result in a person perceiving some sort of void between one’s desire for a social interaction or connection and true experiences of it. \n \n Loneliness can affect us all, even those who are with others through most of the day or in a long-lasting marriage. Evidence suggest that loneliness can result in a serious threat to health and well-being, which is why it is important to seek help from professionals and from others. \n \n See our how to cope page to look at a few things you can do to reduce loneliness. ", 
                        InformationSectionID = 2   
                    }
                }
            });
            InformationSections.Add(new InformationSection
            {
                Id = 3,
                Name = "Useful Links and information",
                Description = "Other links and sources to help",
                Image = image,
                DescriptionSections = new List<DescriptionSection>() 
                { 
                    new DescriptionSection 
                    {
                        ID = 1,
                        DescriptionHeading = "Other links for loneliness.", 
                        DescriptionText = "Loneliness is a challenge and can become even more of a challenge when your dealing with it on yor own. Evidence showns that masking or hiding feelings of loneliness can make symptoms worse, which is why it is important to look for help when possible. \n \n Here are some of the sites are orginisations we recommend:",
                        InformationSectionID = 3
                    },
                    new DescriptionSection
                    {   ID = 2,
                        DescriptionHeading = "The NHS",
                        DescriptionText = "The NHS's website has a lot of good information on loneliness and mental health. It also will provide you with relevant phone numbers to call if your struggling. \n \n Link: https://www.nhs.uk/every-mind-matters/lifes-challenges/loneliness/",
                        InformationSectionID = 3
                    }, 
                    new DescriptionSection
                    {   ID = 3, 
                        DescriptionHeading = "Age Uk",
                        DescriptionText ="Age UK is a registered charity located in the UK. They offer support and help for people dealing with loneliness and offer support lines on their website. \n \n Link: https://www.ageuk.org.uk/",
                        InformationSectionID = 3
                    }, new DescriptionSection 
                    {   ID = 4,
                        DescriptionHeading = "Red Cross",
                        DescriptionText = "The british red cross are an orginisation based in the UK and have helped millions accross the UK struggling with loneliness and isolation. \n \n Link: https://www.redcross.org.uk/about-us/what-we-do/action-on-loneliness ",
                        InformationSectionID = 3
                    }
                }
            });
            return InformationSections;
        }


    }
}
