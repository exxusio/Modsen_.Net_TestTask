
using AutoMapper;
using EventsWebApplication.Application.UseCases.Users.UserCases.Commands.UpdateUser;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Application.Configs.Mappings.Users
{
    public class UpdateUserCommandToUserProfile
    : Profile
    {
        public UpdateUserCommandToUserProfile()
        {
            CreateMap<UpdateUserCommand, User>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.HashPassword, opt => opt.Ignore())
                .ForMember(dest => dest.RoleId, opt => opt.Ignore())
                .ForMember(dest => dest.Role, opt => opt.Ignore())
                .ForMember(dest => dest.EventRegistrations, opt => opt.Ignore())
                .ForMember(dest => dest.RefreshTokens, opt => opt.Ignore());
        }
    }
}