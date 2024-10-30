using EventsWebApplication.Application.DTOs.Tokens;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Application.Algorithms.Interfaces
{
    public interface ITokensGenerator
    {
        Token GenerateAccessToken(User user);
        Token GenerateRefreshToken();
    }
}