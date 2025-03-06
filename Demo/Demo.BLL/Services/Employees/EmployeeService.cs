using Demo.BLL.DTOs.Departments;
using Demo.BLL.DTOs.Employees;
using Demo.DAL.Entities.Employees;
using Demo.DAL.Persistence.Repositories.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Services.Employees
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeService(IEmployeeRepository employeeRepository) {

            _employeeRepository = employeeRepository;
        }
        public int CreateEmployee(EmployeeToCreateDTO Entity)
        {
            Employee employee = new Employee()
            {
                Name = Entity.Name,
                Age = Entity.Age,
                Address = Entity.Address,
                ISActive = Entity.ISActive,
                salary = Entity.Salary,
                Email = Entity.Email,
                PhoneNumber = Entity.PhoneNumber,
                HiringDate = Entity.HiringDate,
                Gender = Entity.Gender,
                EmployeeType = Entity.EmployeeType,
                CreatedBy = 1,
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.UtcNow
            };
           return _employeeRepository.AddT(employee);
        }

        public int UpdateEmployee(EmployeeToUpdateDTO Entity)
        {
            Employee employee = new Employee()
            {
                Id = Entity.Id,
                Name = Entity.Name,
                Age = Entity.Age,
                Address = Entity.Address,
                ISActive = Entity.ISActive,
                salary = Entity.Salary,
                Email = Entity.Email,
                PhoneNumber = Entity.PhoneNumber,
                HiringDate = Entity.HiringDate,
                Gender = Entity.Gender,
                EmployeeType = Entity.EmployeeType,
                CreatedBy = 1,
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.UtcNow
            };
            return _employeeRepository.UpdateT(employee);
        }
        public bool DeleteEmployee(int id)
        {
            var employee = _employeeRepository.GetById(id);
            if (employee is not null)
            {
                return _employeeRepository.DeleteT(employee) > 0;
            }
            return false;

            
           
        }

        public IEnumerable<EmployeeToReturnDto> GetAllEmployees()
        {
            return _employeeRepository.GetAllQueryable().Where(E=>!E.IsDeleted).Select(employee=>new EmployeeToReturnDto()
            {
                Id = employee.Id,
                Name = employee.Name,
                Age = employee.Age,
                salary = employee.salary,
                ISActive = employee.ISActive,
                Email = employee.Email,
                Gender= employee.Gender.ToString(),
                EmployeeType = employee.EmployeeType.ToString(),


            });

        }

        public EmployeeDetailsToReturnDTO? GetEmployeesById(int id)
        {
            var Employee = _employeeRepository.GetById(id);

            if (Employee is not null)
            {
                return new EmployeeDetailsToReturnDTO()
                {
                    Id = Employee.Id,
                    Name = Employee.Name,
                    Address = Employee.Address,
                    Age = Employee.Age,
                    Email = Employee.Email,
                    ISActive = Employee.ISActive,
                    Salary = Employee.salary,
                    PhoneNumber = Employee.PhoneNumber,
                    Gender = Employee.Gender.ToString(),
                    EmployeeType = Employee.EmployeeType.ToString(),


                };
            }

            return null;
        }

       
    }
}
