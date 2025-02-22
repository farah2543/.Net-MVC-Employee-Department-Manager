using Demo.DAL.Entities.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Persistence.Repositories.Departments
{
    public interface IDepartmentRepository
    {
        IEnumerable<Department> GetAll(bool asNoTracking = true);

        IQueryable<Department> GetAllQuerable();
        Department? GetById(int departmentId);
        int AddDepartment (Department Entity);

        int UpdateDepartment(Department Entity);


        int DeleteDepartment(Department Entity);








    }
}
