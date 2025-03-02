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
    internal class DepartmentService : IDepartmentServices
    {
        private readonly IDepartmentRepository _repository;
        public DepartmentService(IDepartmentRepository repository)
        {
            _repository = repository;
            
        }
        public IEnumerable<DepartmentToReturnDto> GetAllDepartments()
        {
            var departments = _repository.GetAllQueryable().Select(department => new DepartmentToReturnDto
            {
                Description = department.Description,
                Name = department.Name,
                Id  = department.Id,
                code= department.code,
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
                    code = department.code,
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
                code = Entity.code,
                Description = Entity.Description,
                Name = Entity.Name,
                LastModifiedBy = 1,
                CreatedBy = 1,
                LastModifiedOn = DateTime.UtcNow


            };

          return  _repository.AddT(department);
                  
          
        }

        public int UpdateDepartment(DepartmentToUpdateDTO Entity)
        {
            var department = new Department()
            {
                code = Entity.code,
                Description = Entity.Description,
                Name = Entity.Name,
                LastModifiedBy = 1,
                CreatedBy = 1,
                LastModifiedOn = DateTime.UtcNow,
               Id = Entity.Id


            };

            return _repository.AddT(department);
        }
        public bool DeleteDepartment(int id)
        {
            var department = _repository.GetById(id);
            if(department is not null)
            {
                return _repository.AddT(department) > 0;
            }
            return false;

        }

     
     
    }
}
