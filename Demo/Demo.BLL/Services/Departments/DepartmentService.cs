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
        public async Task<IEnumerable<DepartmentToReturnDto>> GetAllDepartmentsAsync()
        {
            var departments = await _unitOfWork.DepartmentRepository.GetAllQueryable().Where(D=>!D.IsDeleted).Select(department => new DepartmentToReturnDto
            {
                //Description = department.Description,
                Name = department.Name,
                Id  = department.Id,
                Code= department.Code,
                CreationDate = department.CreationDate,
            }).AsNoTracking().ToListAsync();

            return departments;
          
            
     
        }

        public async Task <DepartmentDetailsToReturnDTO?> GetDepartmentsByIdAsync(int id)
        {
            var department = await _unitOfWork.DepartmentRepository.GetByIdAsync(id);
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

        public async Task<int> CreateDepartmentAsync(DepartmentToCreateDTO Entity)
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

          return await _unitOfWork.CompleteAsync();
                  
          
        }

        public async Task<int> UpdateDepartmentAsync(DepartmentToUpdateDTO Entity)
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

            return await _unitOfWork.CompleteAsync();
        }
        public async Task<bool> DeleteDepartmentAsync(int id)
        {
            var departmentReop = _unitOfWork.DepartmentRepository;
            var department = await departmentReop.GetByIdAsync(id);

            if(department is not null)
            {
                 departmentReop.DeleteT(department);
            }
            return await _unitOfWork.CompleteAsync() > 0 ;

        }

     
     
    }
}
