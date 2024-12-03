
using AutoMapper;
using EventsWebApplication.Application.DTOs.Users;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Application.Configs.Mappings.Users
{
    public class UserToUserReadDtoProfile
    : Profile
    {
        public UserToUserReadDtoProfile()
        {
            CreateMap<User, UserReadDto>();
        }
    }
}