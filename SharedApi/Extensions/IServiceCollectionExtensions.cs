using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace SharedApi;
public static class IServiceCollectionExtensions
{
    public static void AddJwtAuthentication(this IServiceCollection services)
    {
        services.AddAuthentication(opt => {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = JwtConfig.RequireHttpsMetadata;
            options.SaveToken = JwtConfig.SaveToken;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = JwtConfig.TokenValidateIssuer,
                ValidateAudience = JwtConfig.TokenValidateAudience,
                ValidateIssuerSigningKey = JwtConfig.TokenValidateIssuerSigningKey,
                ValidIssuer = "https://localhost:5002",
                ValidAudience = "https://localhost:5002",
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtConfig.TokenSecurityKey))
            };
        });
    }

    public static void AddSwaggerGenWithAuthorize(this IServiceCollection services)
    {
        services.AddSwaggerGen(opt =>
        {
            // opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
            opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                // Description = "Please enter token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "bearer"
            });
            opt.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type=ReferenceType.SecurityScheme,
                            Id="Bearer"
                        }
                    },
                    new string[]{}
                }
            });
        });
    }
}