using AutoMapper;
using EventsWebApplication.Application.DTOs.Roles;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Application.Profiles
{
    public class RoleMappingProfile : Profile
    {
        public RoleMappingProfile()
        {
            CreateMap<RoleCreateDto, Role>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Users, opt => opt.Ignore());

            CreateMap<RoleUpdateDto, Role>()
                .ForMember(dest => dest.Users, opt => opt.Ignore());

            CreateMap<Role, RoleReadDto>();

            CreateMap<Role, RoleDetailedReadDto>()
                .ForMember(dest => dest.Users, opt => opt.MapFrom(src => src.Users));
        }
    }
}