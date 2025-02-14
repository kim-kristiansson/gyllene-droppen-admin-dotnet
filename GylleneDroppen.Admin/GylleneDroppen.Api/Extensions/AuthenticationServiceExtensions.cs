using System.Text;
using GylleneDroppen.Api.Configuration;
using GylleneDroppen.Api.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace GylleneDroppen.Api.Extensions;

public static class AuthenticationServiceExtensions
{
    public static void AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtOptions = configuration.GetSection("JwtOptions").Get<JwtOptions>();
        
        if (jwtOptions == null || string.IsNullOrWhiteSpace(jwtOptions.Secret))
        {
            throw new ArgumentException("JWT Secret is missing in configuration.");
        }

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Secret)),
                ValidIssuer = jwtOptions.Issuer,
                ValidAudience = jwtOptions.Audience,
                ValidateIssuerSigningKey = true,
                ValidateIssuer = !string.IsNullOrWhiteSpace(jwtOptions.Issuer),
                ValidateAudience = !string.IsNullOrWhiteSpace(jwtOptions.Audience),
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
        });
    }
}