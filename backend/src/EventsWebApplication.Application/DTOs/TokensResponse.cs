using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Application.DTOs
{
    public class TokensResponse
    {
        public Token AccessToken { get; set; }
        public Token RefreshToken { get; set; }
    }
}