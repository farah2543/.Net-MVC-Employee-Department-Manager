using Demo.DAL.Entities.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.DTOs.Employees
{
    public class EmployeeDetailsToReturnDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int? Age { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public bool ISActive { get; set; }
        public decimal Salary { get; set; }
        public string Gender { get; set; }
        public string EmployeeType { get; set; }

    }
}
