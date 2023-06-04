using AutoMapper;
using LibraryManagement.Application.Models;

namespace LibraryManagement.Webapp.Dto
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AuthorDto, Author>();
            CreateMap<Author, AuthorDto>();

            CreateMap<EmployeeDto, Employee>();
            CreateMap<Employee, EmployeeDto>();
        }
    }
}
