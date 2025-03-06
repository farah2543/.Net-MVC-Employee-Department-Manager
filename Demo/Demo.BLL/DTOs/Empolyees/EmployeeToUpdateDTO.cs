using Demo.DAL.Entities.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.DTOs.Employees
{
    public class EmployeeToUpdateDTO
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "The Employee Name Maximum Length is 50 characters")]
        [MinLength(5, ErrorMessage = "The Employee Name Minimum Length is 5  characters")]
        public string Name { get; set; } = null!;
        [Range(22, 30)]
        public int? Age { get; set; }
        [RegularExpression(@"^[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{4,10}-[a-zA-Z]{5,10}$",
            ErrorMessage = " Address Must be like 123-Street-City-County")]
        public string? Address { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        [Display(Name = "Is Active")]
        public bool ISActive { get; set; }

        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }

        [Display(Name = "Phone Number")]
        [Phone]
        public string? PhoneNumber { get; set; }

        [Display(Name = "Hiring Date")]

        public DateOnly? HiringDate { get; set; }
        public Gender Gender { get; set; }
        public EmployeeType EmployeeType { get; set; }

    }
}
