using AutoMapper;
using EventsWebApplication.Application.DTOs.Tokens;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Application.MappingConfigurations
{
    public class RefreshTokenMappingProfile : Profile
    {
        public RefreshTokenMappingProfile()
        {
            CreateMap<Token, RefreshToken>()
                .ForMember(dest => dest.Key, opt => opt.MapFrom(src => src.Value))
                .ForMember(dest => dest.ExpirationTime, opt => opt.MapFrom(src => src.ExpiresIn))
                .ForMember(dest => dest.CreationTime, opt => opt.MapFrom(src => DateTime.UtcNow));
        }
    }
}