using AutoMapper;
using Demo.BLL.DTOs.Departments;
using Demo.BLL.DTOs.Employees;
using Demo.PL.ViewModels.Department;
using Demo.PL.ViewModels.Employee;

namespace Demo.PL.Mapper.Profiles
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            #region Department Module
            CreateMap<DepartmentViewModel, DepartmentToCreateDTO>()/*.ReverseMap()*/;

            CreateMap<DepartmentViewModel, DepartmentToUpdateDTO>();

            CreateMap<DepartmentDetailsToReturnDTO, DepartmentViewModel>();



            #endregion


            #region Employee Module 

            CreateMap<EmployeeViewModel, EmployeeToCreateDTO>();

            CreateMap<EmployeeViewModel, EmployeeToUpdateDTO>();

            CreateMap<EmployeeDetailsToReturnDTO, EmployeeViewModel>()
                .ForMember(dest => dest.Image, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image)).ReverseMap();



            #endregion


        }

    }
}
