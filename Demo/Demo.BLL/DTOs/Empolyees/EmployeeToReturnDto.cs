using Demo.DAL.Entities.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.DTOs.Employees
{
    public class EmployeeToReturnDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int? Age { get; set; }

        [DataType(DataType.Currency)]
        public decimal salary { get; set; }
        [EmailAddress]
        public string? Email { get; set; }

        [Display(Name = "IS Active")]
        public bool ISActive { get; set; }
        public string Gender { get; set; }

        [Display(Name ="Employee Type")]
        public string EmployeeType { get; set; }
    }
}
