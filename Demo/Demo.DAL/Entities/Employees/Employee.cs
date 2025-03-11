using Demo.DAL.Entities.Common.Enums;
using Demo.DAL.Entities.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Entities.Employees
{
    public class Employee : ModelBase
    {
        public string Name { get; set; } = null!;
        public int? Age { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public bool ISActive { get; set; }
        public decimal salary { get; set; }
        public string? PhoneNumber { get; set; }
        public DateOnly HiringDate { get; set; }
        public Gender Gender { get; set; }
        public EmployeeType EmployeeType { get; set; }


        // Navigational Property 
        public virtual Department? Department {  get; set; }  
       
        //Foreign key 

        public int ? DepartmentID { get; set; }



        

    }
}
