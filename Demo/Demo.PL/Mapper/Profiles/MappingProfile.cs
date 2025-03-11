using AutoMapper;
using Demo.BLL.DTOs.Departments;
using Demo.PL.ViewModels.Department;

namespace Demo.PL.Mapper.Profiles
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            #region Employee Module
            CreateMap<DepartmentViewModel, DepartmentToCreateDTO>()/*.ReverseMap()*/;

            CreateMap<DepartmentDetailsToReturnDTO, DepartmentViewModel>();


            CreateMap<DepartmentViewModel, DepartmentToUpdateDTO>();



            #endregion


            #region Department Module 
            #endregion


        }

    }
}
