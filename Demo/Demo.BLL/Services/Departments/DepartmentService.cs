using Demo.BLL.DTOs.Departments;
using Demo.DAL.Entities.Departments;
using Demo.DAL.Persistence.Repositories.Departments;
using Demo.DAL.Persistence.UnitOfWork;
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
        private readonly IUnitOfWork _unitOfWork;

        //private readonly IDepartmentRepository _repository;
        //public DepartmentService(IDepartmentRepository repository)
        //{
        //    _repository = repository;

        //}

        public DepartmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<DepartmentToReturnDto> GetAllDepartments()
        {
            var departments = _unitOfWork.DepartmentRepository.GetAllQueryable().Where(D=>!D.IsDeleted).Select(department => new DepartmentToReturnDto
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
            var department = _unitOfWork.DepartmentRepository.GetById(id);
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
            _unitOfWork.DepartmentRepository.AddT(department);

          return  _unitOfWork.Complete();
                  
          
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

            _unitOfWork.DepartmentRepository.UpdateT(department);

            return _unitOfWork.Complete();
        }
        public bool DeleteDepartment(int id)
        {
            var departmentReop = _unitOfWork.DepartmentRepository;
            var department = departmentReop.GetById(id);
            if(department is not null)
            {
                 departmentReop.DeleteT(department);
            }
            return _unitOfWork.Complete() > 0 ;

        }

     
     
    }
}
