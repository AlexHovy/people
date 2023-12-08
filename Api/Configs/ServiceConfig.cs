using System.Text;
using Api.DbContexts;
using Api.Queries;
using Api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Api.Configs;

public static class ServiceConfig
{
    public static void AddServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton(configuration);
        services.AddSingleton<ConfigService>();
        services.AddSingleton<AuthService>();
        services.AddSingleton<PersonQuery>();
    }

    public static void AddDbContexts(IServiceCollection services)
    {
        services.AddDbContext<PeopleContext>(options =>
        {
            var serviceProvider = services.BuildServiceProvider();
            var configService = serviceProvider.GetService<ConfigService>();

            string connectionString = configService.GetConnectionString();
            options.UseSqlServer(connectionString);
        });
    }

    public static void AddAuth(WebApplicationBuilder builder)
    {
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                var serviceProvider = builder.Services.BuildServiceProvider();
                var configService = serviceProvider.GetService<ConfigService>();
                var jwtSettings = configService.GetJwtSettings();

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key))
                };
            });
    }

    public static void AddControllers(IServiceCollection services)
    {
        services.AddControllers();
    }
}
