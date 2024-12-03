using AutoMapper;
using EventsWebApplication.Application.UseCases.Users.UserCases.Commands.CreateUser;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Application.Configs.Mappings.Users
{
    public class CreateUserCommandToUserProfile
    : Profile
    {
        public CreateUserCommandToUserProfile()
        {
            CreateMap<CreateUserCommand, User>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.HashPassword, opt => opt.Ignore())
                .ForMember(dest => dest.DateOfBirth, opt => opt.Ignore())
                .ForMember(dest => dest.RoleId, opt => opt.Ignore())
                .ForMember(dest => dest.Role, opt => opt.Ignore())
                .ForMember(dest => dest.EventRegistrations, opt => opt.Ignore())
                .ForMember(dest => dest.RefreshTokens, opt => opt.Ignore());
        }
    }
}