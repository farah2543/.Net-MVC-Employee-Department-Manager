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
        Task <IEnumerable<EmployeeToReturnDto>> GetAllEmployeesAsync(string SearchValue);

        Task <EmployeeDetailsToReturnDTO?> GetEmployeesByIdAsync(int id);

        Task <int > CreateEmployeeAsync(EmployeeToCreateDTO Entity);


        Task<int> UpdateEmployeeAsync(EmployeeToUpdateDTO Entity);


        Task<bool> DeleteEmployeeAsync(int id);

    }
}
