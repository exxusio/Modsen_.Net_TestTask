namespace EventsWebApplication.Application.DTOs.Tokens
{
    public class TokensResponse
    {
        public Token AccessToken { get; set; }

        public Token RefreshToken { get; set; }
    }
}