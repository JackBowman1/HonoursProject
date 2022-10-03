using System;
using System.Collections.Generic;
using System.Text;

namespace HonoursProject.Models
{
    public class Staff : User
    {
        public double Salary { get; set; }
        public string JobRole { get; set; }

        public Staff() 
        {
            this.Salary = 20.00;
            this.JobRole = "Admin";
        }

        public Staff(double salaryIn, string jobRoleIn) 
        {
            this.Salary = salaryIn;
            this.JobRole = jobRoleIn;
        
        }
    }
}
