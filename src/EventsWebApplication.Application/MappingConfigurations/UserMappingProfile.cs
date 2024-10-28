using AutoMapper;
using EventsWebApplication.Application.DTOs.Users;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Application.Profiles
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            // CreateMap<UserCreateDto, User>()
            //     .ForMember(dest => dest.Id, opt => opt.Ignore())
            //     .ForMember(dest => dest.Email, opt => opt.Ignore())
            //     .ForMember(dest => dest.RoleId, opt => opt.Ignore())
            //     .ForMember(dest => dest.Role, opt => opt.Ignore())
            //     .ForMember(dest => dest.EventRegistrations, opt => opt.Ignore());

            // CreateMap<UserUpdateDto, User>()
            //     .ForMember(dest => dest.Role, opt => opt.Ignore())
            //     .ForMember(dest => dest.EventRegistrations, opt => opt.Ignore());

            // CreateMap<User, UserReadDto>();

            // CreateMap<User, UserDetailedReadDto>()
            //     .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role))
            //     .ForMember(dest => dest.EventRegistrations, opt => opt.MapFrom(src => src.EventRegistrations));
        }
    }
}