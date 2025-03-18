using Demo.BLL.DTOs.Departments;
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
        Task<IEnumerable<DepartmentToReturnDto>> GetAllDepartmentsAsync();

       Task<DepartmentDetailsToReturnDTO?> GetDepartmentsByIdAsync(int id);
        Task<int> CreateDepartmentAsync(DepartmentToCreateDTO Entity);


        Task<int> UpdateDepartmentAsync(DepartmentToUpdateDTO Entity);


        Task<bool> DeleteDepartmentAsync(int id);


    }
}
