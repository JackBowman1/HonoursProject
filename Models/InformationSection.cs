using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace HonoursProject.Models
{
    public class InformationSection
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<DescriptionSection> DescriptionSections { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public InformationSection() 
        {
            Id = 1;
            Name = "hey";
            DescriptionSections = new List<DescriptionSection>();
            Image = "";
        
        }


    }
}
