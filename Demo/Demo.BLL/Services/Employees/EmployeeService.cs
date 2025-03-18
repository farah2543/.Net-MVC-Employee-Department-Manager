using Demo.BLL.Common.Services.AttachmentServices;
using Demo.BLL.DTOs.Departments;
using Demo.BLL.DTOs.Employees;
using Demo.DAL.Entities.Employees;
using Demo.DAL.Persistence.Repositories.Employees;
using Demo.DAL.Persistence.UnitOfWork;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAttachmentService _attachmentService;

        //private readonly IEmployeeRepository _employeeRepository;
        //public EmployeeService(IEmployeeRepository employeeRepository) {

        //    _employeeRepository = employeeRepository;
        //}

        public EmployeeService(IUnitOfWork unitOfWork,IAttachmentService attachmentService)
        {
            _unitOfWork = unitOfWork;
           _attachmentService = attachmentService;
        }


        public async Task<int> CreateEmployeeAsync(EmployeeToCreateDTO Entity)
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
            if(Entity.Image is not null)
            {
                employee.Image = await _attachmentService.UploadAsync(Entity.Image,"images");
            }
            _unitOfWork.EmployeeRepository.AddT(employee);
            return await _unitOfWork.CompleteAsync();
        }

        public async Task<int> UpdateEmployeeAsync(EmployeeToUpdateDTO Entity)
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
            if (Entity.Image is not null)
            {
                employee.Image = await _attachmentService.UploadAsync(Entity.Image, "images");
            }
            _unitOfWork.EmployeeRepository.UpdateT(employee);
            return await _unitOfWork.CompleteAsync();
        }
        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            var employeeRepo =  _unitOfWork.EmployeeRepository;
            var employee = await employeeRepo.GetByIdAsync(id);
            if (employee is not null)
            {
                 employeeRepo.DeleteT(employee);
            }


            if (employee.Image is not null)
            {
               _attachmentService.DeleteAsync(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files",employee.Image));
            }


            return await _unitOfWork.CompleteAsync() > 0;




        }

        public async Task< IEnumerable<EmployeeToReturnDto>> GetAllEmployeesAsync(string SearchValue)
        {
            return await _unitOfWork.EmployeeRepository.GetAllQueryable().
                Include(E => E.Department).
                Where(E => !E.IsDeleted &&
                (string.IsNullOrEmpty(SearchValue) ||
                E.Name.ToLower().Contains(SearchValue.ToLower()))).

                Select(employee => new EmployeeToReturnDto()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Age = employee.Age,
                    salary = employee.salary,
                    ISActive = employee.ISActive,
                    Email = employee.Email,
                    Gender = employee.Gender.ToString(),
                    EmployeeType = employee.EmployeeType.ToString(),
                    Department = employee.Department.Name, //Eager Loading
                    Image = employee.Image,


                }).ToListAsync();

        }

        //public IEnumerable<EmployeeToReturnDto> GetAllEmployeesAsync()
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

        public async Task<EmployeeDetailsToReturnDTO?> GetEmployeesByIdAsync(int id)
        {
            var Employee = await _unitOfWork.EmployeeRepository.GetByIdAsync(id);

            if (Employee is not null)
            {
                return  new EmployeeDetailsToReturnDTO()
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
                    Department= Employee.Department?.Name??"No Department", // Lazy Loading
                    Image = Employee.Image,



                };
            }

            return null;
        }

       
    }
}
