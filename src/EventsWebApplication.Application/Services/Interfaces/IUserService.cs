using EventsWebApplication.Application.DTOs.Users;
using EventsWebApplication.Domain.Interfaces;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Application.Services.Interfaces
{
    public interface IUserService : IService<User, UserReadDto, UserDetailedReadDto, UserCreateDto, UserUpdateDto>
    {

    }
}