using InnoGotchi.API.Entities.DataTransferObjects;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace InnoGotchi.API.Extensions
{
    public class JwtAuthMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public JwtAuthMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();

            if (authHeader != null)
            {
                var token = authHeader.Split(" ").Last();

                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

                TokenValidationParameters validationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = AuthOptions.ISSUER,
                    ValidateAudience = true,
                    ValidAudience = AuthOptions.AUDIENCE,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Secret").Value)),
                    ValidateIssuerSigningKey = true,
                };

                tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;

                UserClaims userInfo = new ()
                {
                    Id = Guid.Parse(jwtToken.Claims.Where(c => c.Type == "Id").Single().Value),
                    Login = jwtToken.Claims.Where(c => c.Type == "Login").Single().Value,
                    Role = jwtToken.Claims.Where(c => c.Type == "Role").Single().Value,
                    OwnFarm = jwtToken.Claims.Where(c => c.Type == "OwnFarm").Single().Value
                };

                context.Items["User"] = userInfo;
            }

            await _next(context);
        }
    }
}
