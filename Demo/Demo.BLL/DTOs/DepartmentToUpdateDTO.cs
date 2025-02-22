using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.DTOs
{
    public class DepartmentToUpdateDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public string code { get; set; } = null!;

        public DateOnly CreationDate { get; set; }

    }
}
