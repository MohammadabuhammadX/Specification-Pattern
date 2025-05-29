using API.Dtos;
using AutoMapper;
using Core.Entities;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Employee, EmployeeToReturnDto>()
                .ForMember(d => d.DepartmentName, o => o.MapFrom(s => s.Department.Name))
                .ForMember(d => d.RoleName, o => o.MapFrom(s => s.Role.Name));
            CreateMap<CreateEmployeeDto, Employee>();

            CreateMap<UpdateEmployeeDto, Employee>();

            CreateMap<Department, DepartmentDto>();

            CreateMap<CreateDepartmentDto, Department>();
            CreateMap<UpdateDepartmentDto, Department>();

        }
    }
}
