using Demo.DAL.Entities.Employees;
using Demo.DAL.Persistence.Data;
using Demo.DAL.Persistence.Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Persistence.Repositories.Employees
{
    public class EmployeeRepository :GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(ApplicationDbContext applicationDbContext) :base(applicationDbContext) 
        {

        }
       
    }
}
