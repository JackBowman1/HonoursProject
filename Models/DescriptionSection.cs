using SQLite;
using System;
using System.Collections.Generic;
using System.Text;


namespace HonoursProject.Models
{
    public class DescriptionSection
    {
        public int ID { get; set; }
        public string DescriptionHeading { get; set; }
        public string DescriptionText { get; set; }

        public int InformationSectionID { get; set; }

    }
}
