using Demo.BLL.DTOs.Departments;
using Demo.BLL.DTOs.Employees;
using Demo.DAL.Entities.Employees;
using Demo.DAL.Persistence.Repositories.Employees;
using Microsoft.EntityFrameworkCore;
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
                CreatedBy = 1,
                Age = Entity.Age,
                LastModifiedBy = 1,
                Name = Entity.Name,
                Email = Entity.Email,
                salary = Entity.Salary,
                Gender = Entity.Gender,
                Address = Entity.Address,
                ISActive = Entity.ISActive,
                LastModifiedOn = DateTime.UtcNow,
                PhoneNumber = Entity.PhoneNumber,
                DepartmentID = Entity.DepartmentId,
                EmployeeType = Entity.EmployeeType,


            };
           return _employeeRepository.AddT(employee);
        }

        public int UpdateEmployee(EmployeeToUpdateDTO Entity)
        {
            Employee employee = new Employee()
            {
                CreatedBy = 1,
                Id = Entity.Id,
                Age = Entity.Age,
                LastModifiedBy = 1,
                Name = Entity.Name,
                Email = Entity.Email,
                salary = Entity.Salary,
                Gender = Entity.Gender,
                Address = Entity.Address,
                ISActive = Entity.ISActive,
                PhoneNumber = Entity.PhoneNumber,
                LastModifiedOn = DateTime.UtcNow,
                DepartmentID = Entity.DepartmentId,
                EmployeeType = Entity.EmployeeType,
              
           

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
            return _employeeRepository.GetAllQueryable().Include(E=>E.Department).Where(E=>!E.IsDeleted).Select(employee=>new EmployeeToReturnDto()
            {
                Id = employee.Id,
                Name = employee.Name,
                Age = employee.Age,
                salary = employee.salary,
                ISActive = employee.ISActive,
                Email = employee.Email,
                Gender= employee.Gender.ToString(),
                EmployeeType = employee.EmployeeType.ToString(),
                Department = employee.Department.Name  //Eager Loading


            });

        }

        //public IEnumerable<EmployeeToReturnDto> GetAllEmployees()
        //{
        //    var query = _employeeRepository.GetAllEnumrable().Where(E => !E.IsDeleted).Select(employee => new EmployeeToReturnDto()
        //    {
        //        Id = employee.Id,
        //        Name = employee.Name,
        //        Age = employee.Age,
        //        salary = employee.salary,
        //        ISActive = employee.ISActive,
        //        Email = employee.Email,
        //        Gender = employee.Gender.ToString(),
        //        EmployeeType = employee.EmployeeType.ToString(),


        //    });
        //    var Employees = query.ToList();
        //    var count = query.Count();
        //    var firstEmployee = query.FirstOrDefault();

        //    return query;

        //}

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
                    HiringDate = Employee.HiringDate,
                    CreatedBy = Employee.CreatedBy,
                    CreateOn = Employee.CreateOn,
                    LastModifiedBy = Employee.LastModifiedBy,
                    LastModifiedOn = Employee.LastModifiedOn,
                    Department= Employee.Department.Name, // Lazy Loading



                };
            }

            return null;
        }

       
    }
}
