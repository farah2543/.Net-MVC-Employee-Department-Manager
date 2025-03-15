using Demo.BLL.DTOs;
using Demo.BLL.DTOs.Departments;
using Demo.BLL.DTOs.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Services.Employees
{
    public interface IEmployeeService
    {
        IEnumerable<EmployeeToReturnDto> GetAllEmployees(string SearchValue);

        EmployeeDetailsToReturnDTO? GetEmployeesById(int id);

        int CreateEmployee(EmployeeToCreateDTO Entity);


        int UpdateEmployee(EmployeeToUpdateDTO Entity);


        bool DeleteEmployee(int id);

    }
}
