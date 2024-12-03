using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using EventsWebApplication.Application.Exceptions;
using EventsWebApplication.Domain.Abstractions.Auth;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Infrastructure.Auth
{
    public class TokensGenerator(
        IConfiguration configuration
    ) : ITokensGenerator
    {
        public Token GenerateAccessToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Login),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role.Name.ToString())
            };

            var tokenExpires = DateTime.UtcNow.AddMinutes(GetJwtSetting<double>("AccessTokenExpiresInMinutes"));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(GetJwtSetting<string>("Key")));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: tokenExpires,
                issuer: GetJwtSetting<string>("Issuer"),
                signingCredentials: credentials);

            return new Token
            {
                Value = new JwtSecurityTokenHandler().WriteToken(token),
                ExpiresIn = tokenExpires
            };
        }

        public Token GenerateRefreshToken()
        {
            return new Token
            {
                Value = Guid.NewGuid().ToString(),
                ExpiresIn = DateTime.UtcNow.AddMinutes(GetJwtSetting<double>("RefreshTokenExpiresInMinutes"))
            };
        }

        private T GetJwtSetting<T>(string key)
        {
            var value = configuration.GetValue<T>($"JwtSettings:{key}");
            if (value == null)
            {
                throw new NotFoundException(
                    "The configuration key is missing or null",
                    typeof(T).Name,
                    nameof(key),
                    key
                );
            }
            return value;
        }
    }
}