using AutoMapper;
using Demo.BLL.DTOs.Departments;
using Demo.BLL.DTOs.Employees;
using Demo.PL.ViewModels.Department;

namespace Demo.PL.Mapper.Profiles
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            #region Department Module
            CreateMap<DepartmentViewModel, DepartmentToCreateDTO>()/*.ReverseMap()*/;

            CreateMap<DepartmentDetailsToReturnDTO, DepartmentViewModel>();


            CreateMap<DepartmentViewModel, DepartmentToUpdateDTO>();



            #endregion


            #region Employee Module 

            CreateMap<EmployeeToCreateUpdateDTO, EmployeeToCreateDTO>();

            CreateMap<EmployeeDetailsToReturnDTO, EmployeeToCreateUpdateDTO>();



            #endregion


        }

    }
}
