using Demo.DAL.Entities.Common.Enums;
using System;
using System.Collections.Generic;
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
        public decimal salary { get; set; }
        public string? Email { get; set; }
        public bool ISActive { get; set; }
        public string Gender { get; set; }
        public string EmployeeType { get; set; }
    }
}
