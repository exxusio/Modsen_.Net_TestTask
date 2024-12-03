
using AutoMapper;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Application.Configs.Mappings.Tokens
{
    public class TokenToRefreshTokenProfile
    : Profile
    {
        public TokenToRefreshTokenProfile()
        {
            CreateMap<Token, RefreshToken>()
                .ForMember(dest => dest.Key, opt => opt.MapFrom(src => src.Value))
                .ForMember(dest => dest.ExpirationTime, opt => opt.MapFrom(src => src.ExpiresIn))
                .ForMember(dest => dest.CreationTime, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<RefreshToken, Token>()
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Key))
                .ForMember(dest => dest.ExpiresIn, opt => opt.MapFrom(src => src.ExpirationTime));
        }
    }
}