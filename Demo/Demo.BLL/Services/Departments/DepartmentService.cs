using Demo.BLL.DTOs;
using Demo.DAL.Entities.Departments;
using Demo.DAL.Persistence.Repositories.Departments;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Services.Departments
{
    public class DepartmentService : IDepartmentServices
    {
        private readonly IDepartmentRepository _repository;
        public DepartmentService(IDepartmentRepository repository)
        {
            _repository = repository;
            
        }
        public IEnumerable<DepartmentToReturnDto> GetAllDepartments()
        {
            var departments = _repository.GetAllQuerable().Select(department => new DepartmentToReturnDto
            {
                //Description = department.Description,
                Name = department.Name,
                Id  = department.Id,
                Code= department.Code,
                CreationDate = department.CreationDate,
            }).AsNoTracking().ToList();

            return departments;
          
            
     
        }

        public DepartmentDetailsToReturnDTO? GetDepartmentsById(int id)
        {
            var department = _repository.GetById(id);
            if (department is not null)
            {
                return new DepartmentDetailsToReturnDTO()
                {
                    Id = department.Id,
                    Name = department.Name,
                    Code = department.Code,
                    CreationDate = department.CreationDate,
                    CreatedBy = department.CreatedBy,
                    CreateOn = department.CreateOn,
                    LastModifiedBy = department.LastModifiedBy,
                    LastModifiedOn = department.LastModifiedOn,
                    Description = department.Description,   
                    IsDeleted = department.IsDeleted,
                   
                };

            }
            return null;

        }

        public int CreateDepartment(DepartmentToCreateDTO Entity)
        {
            var department = new Department()
            {
                Code = Entity.Code,
                Description = Entity.Description,
                Name = Entity.Name,
                LastModifiedBy = 1,
                CreatedBy = 1,
                LastModifiedOn = DateTime.UtcNow


            };

          return  _repository.AddDepartment(department);
                  
          
        }

        public int UpdateDepartment(DepartmentToUpdateDTO Entity)
        {
            var department = new Department()
            {
                Code = Entity.Code,
                Description = Entity.Description,
                Name = Entity.Name,
                LastModifiedBy = 1,
                CreatedBy = 1,
                LastModifiedOn = DateTime.UtcNow,
               Id = Entity.Id


            };

            return _repository.UpdateDepartment(department);
        }
        public bool DeleteDepartment(int id)
        {
            var department = _repository.GetById(id);
            if(department is not null)
            {
                return _repository.DeleteDepartment(department) > 0;
            }
            return false;

        }

     
     
    }
}
