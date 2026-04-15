using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace B2B.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services
            //.AddHttpContextAccessor()
            .AddServices()
            .AddAuthentication(configuration)
            .AddAuthorization()
            .AddPersistence();

        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        //services.AddSingleton<IDateTimeProvider, SystemDateTimeProvider>();

        return services;
    }

    private static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        //services.AddDbContext<AppDbContext>(options => options.UseSqlite("Data Source = CleanArchitecture.sqlite"));

        //services.AddScoped<IRemindersRepository, RemindersRepository>();
        //services.AddScoped<IUsersRepository, UsersRepository>();

        return services;
    }

    private static IServiceCollection AddAuthorization(this IServiceCollection services)
    {
        // services.AddScoped<IAuthorizationService, AuthorizationService>();
        //  services.AddScoped<ICurrentUserProvider, CurrentUserProvider>();
        // services.AddSingleton<IPolicyEnforcer, PolicyEnforcer>();

        return services;
    }

    private static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        //services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.Section));

        //services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

        //services
        //    .ConfigureOptions<JwtBearerTokenValidationConfiguration>()
        //    .AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
        //    .AddJwtBearer();

        return services;
    }
}
