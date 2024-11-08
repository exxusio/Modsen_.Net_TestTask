using AutoMapper;
using EventsWebApplication.Application.DTOs;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Application.Configs.Mappings
{
    public class RoleMappingConfig
    : Profile
    {
        public RoleMappingConfig()
        {
            CreateMap<Role, RoleReadDto>();
        }
    }
}