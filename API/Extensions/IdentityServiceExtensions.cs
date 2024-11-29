using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace API.Extensions;

public static class IdentityServiceExtensions
{
    public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            var tokenKey = config["TokenKey"] ?? throw new Exception("Token Key not found");
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey)),
                ValidateIssuer = false,
                ValidateAudience = false
            };

            // Interceptar el token desde los parámetros de consulta
            options.Events = new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    // Buscar el token en el parámetro "auth"
                    var token = context.Request.Query["auth"];
                    if (!string.IsNullOrEmpty(token))
                    {
                        context.Token = token; // Asignar el token al contexto
                    }
                    return Task.CompletedTask;
                }
            };
        });

        return services;
    }
}
