using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Domain.Abstractions.Auth
{
    public interface ITokensGenerator
    {
        Token GenerateAccessToken(User user);
        Token GenerateRefreshToken();
    }
}