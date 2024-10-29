using AutoMapper;
using EventsWebApplication.Application.UseCases.Users.UserCases.Commands.CreateUser;
using EventsWebApplication.Application.UseCases.Users.UserCases.Commands.UpdateUser;
using EventsWebApplication.Application.DTOs;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Application.Profiles
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<CreateUserCommand, User>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.HashPassword, opt => opt.Ignore())
                .ForMember(dest => dest.DateOfBirth, opt => opt.Ignore())
                .ForMember(dest => dest.RoleId, opt => opt.Ignore())
                .ForMember(dest => dest.Role, opt => opt.Ignore())
                .ForMember(dest => dest.EventRegistrations, opt => opt.Ignore())
                .ForMember(dest => dest.RefreshTokens, opt => opt.Ignore());

            CreateMap<User, UserReadDto>();

            CreateMap<UpdateUserCommand, User>()
                .ForMember(dest => dest.HashPassword, opt => opt.Ignore())
                .ForMember(dest => dest.RoleId, opt => opt.Ignore())
                .ForMember(dest => dest.Role, opt => opt.Ignore())
                .ForMember(dest => dest.EventRegistrations, opt => opt.Ignore())
                .ForMember(dest => dest.RefreshTokens, opt => opt.Ignore());
        }
    }
}