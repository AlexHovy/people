using System.Text;
using Api.DbContexts;
using Api.Interfaces;
using Api.Queries;
using Api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Api.Configs;

public static class StartupConfig
{
    public static void AddServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton(configuration);
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<ConfigService>();
        services.AddScoped<AuthService>();
        services.AddScoped<PersonService>();
        services.AddScoped<PersonQuery>();
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

    public static void AddDbSeedData(WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;

            try
            {
                var context = services.GetRequiredService<PeopleContext>();
                DbInitialiser.Initialize(context);
            }
            catch (Exception ex)
            {
                // Handle exceptions, this is useful to debug issues during seeding
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred while seeding the database.");
            }
        }
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
