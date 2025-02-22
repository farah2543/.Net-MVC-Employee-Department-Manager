using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.DTOs
{
    public class DepartmentToCreateDTO
    {
        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public string code { get; set; } = null!;

        public DateOnly CreationDate { get; set; }

        public int Id { get; set; }

        public int CreatedBy { get; set; }
        public DateTime CreateOn { get; set; }
        public int LastModifiedBy { get; set; }
        public DateTime LastModifiedOn { get; set; }

        public bool IsDeleted { get; set; }
    }
}
