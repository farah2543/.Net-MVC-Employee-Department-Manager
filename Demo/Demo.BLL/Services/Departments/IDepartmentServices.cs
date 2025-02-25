using Demo.BLL.DTOs;
using Demo.DAL.Entities.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Services.Departments
{
    public interface IDepartmentServices
    {
        IEnumerable<DepartmentToReturnDto> GetAllDepartments();

        DepartmentDetailsToReturnDTO? GetDepartmentsById(int id);

        int CreateDepartment(DepartmentToCreateDTO Entity);


        int UpdateDepartment(DepartmentToUpdateDTO Entity);


        bool DeleteDepartment(int id);


    }
}
