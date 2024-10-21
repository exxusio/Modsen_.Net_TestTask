using EventsWebApplication.Application.DTOs.Roles;
using EventsWebApplication.Domain.Interfaces;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Application.Services.Interfaces
{
    public interface IRoleService : IService<Role, RoleReadDto, RoleDetailedReadDto, RoleCreateDto, RoleUpdateDto>
    {

    }
}