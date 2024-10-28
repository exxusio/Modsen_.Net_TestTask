using AutoMapper;
using EventsWebApplication.Application.DTOs;
using EventsWebApplication.Application.UseCases.Admins.RoleCases.Commands.CreateRole;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Application.MappingConfigurations
{
    public class RoleMappingProfile : Profile
    {
        public RoleMappingProfile()
        {
            CreateMap<CreateRoleCommand, Role>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Users, opt => opt.Ignore());

            CreateMap<Role, RoleReadDto>();
        }
    }
}