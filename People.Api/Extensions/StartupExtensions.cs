using System.Text;
using People.Data.DbContexts;
using People.Services.Services.Interfaces;
using People.Services.Services;
using People.Services.Queries.Interfaces;
using People.Services.Queries;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace People.Api.Extensions;

public static class StartupExtensions
{
    public static void AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton(configuration);

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IConfigService, ConfigService>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IFileService, FileService>();
        services.AddScoped<IPersonService, PersonService>();
        services.AddScoped<IPersonQuery, PersonQuery>();
        services.AddScoped<ICountryQuery, CountryQuery>();
        services.AddScoped<ICityQuery, CityQuery>();
    }

    public static void AddDbContexts(this IServiceCollection services)
    {
        services.AddDbContext<PeopleContext>(options =>
        {
            var serviceProvider = services.BuildServiceProvider();
            
            var configService = serviceProvider.GetService<IConfigService>();
            if (configService is null)
                throw new ArgumentNullException(nameof(configService));

            string connectionString = configService.GetConnectionString();
            options.UseSqlServer(connectionString);
        });
    }

    public static void AddDbSeedData(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;

            try
            {
                var context = services.GetRequiredService<PeopleContext>();
                DbInitialiser.Initialise(context);
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred while seeding the database.");
            }
        }
    }

    public static void AddAuth(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                var serviceProvider = builder.Services.BuildServiceProvider();

                var configService = serviceProvider.GetService<IConfigService>();
                if (configService is null)
                    throw new ArgumentNullException(nameof(configService));

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
}
