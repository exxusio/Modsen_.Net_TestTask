
using AutoMapper;
using EventsWebApplication.Application.DTOs;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Application.Configs.Mappings.Roles
{
    public class RoleToRoleReadDtoProfile
    : Profile
    {
        public RoleToRoleReadDtoProfile()
        {
            CreateMap<Role, RoleReadDto>();
        }
    }
}