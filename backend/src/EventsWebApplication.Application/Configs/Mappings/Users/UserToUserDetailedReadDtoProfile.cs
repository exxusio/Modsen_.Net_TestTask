
using AutoMapper;
using EventsWebApplication.Application.DTOs.Users;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Application.Configs.Mappings.Users
{
    public class UserToUserDetailedReadDtoProfile
    : Profile
    {
        public UserToUserDetailedReadDtoProfile()
        {
            CreateMap<User, UserDetailedReadDto>()
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.Name));
        }
    }
}