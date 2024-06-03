using BookShop.Configuration.Infrastructure.AspnetCoreIdentityOption;
using BookShop.Data.Shared.Entities;
using BookShop.SqlServer.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookShop.SqlServer.ServiceConfigs;

/// <summary>
///     AspnetCore Identity service config.
/// </summary>
internal static class AspNetCoreIdentityServiceConfig
{
    /// <summary>
    ///     Configure asp net core identity service.
    /// </summary>
    /// <param name="services">
    ///     Service container.
    /// </param>
    /// <param name="configuration">
    ///     Load configuration for configuration
    ///     file (appsetting).
    /// </param>
    internal static void ConfigureAspNetCoreIdentity(
        this IServiceCollection services,
        IConfigurationManager configuration
    )
    {
        services
            .AddIdentity<User, Role>(setupAction: config =>
            {
                var option = configuration
                    .GetRequiredSection(key: "AspNetCoreIdentity")
                    .Get<AspNetCoreIdentityOption>();

                config.Password.RequireDigit = option.Password.RequireDigit;
                config.Password.RequireLowercase = option.Password.RequireLowercase;
                config.Password.RequireNonAlphanumeric = option.Password.RequireNonAlphanumeric;
                config.Password.RequireUppercase = option.Password.RequireUppercase;
                config.Password.RequiredLength = option.Password.RequiredLength;
                config.Password.RequiredUniqueChars = option.Password.RequiredUniqueChars;

                config.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(
                    value: option.Lockout.DefaultLockoutTimeSpanInSecond
                );
                config.Lockout.MaxFailedAccessAttempts = option.Lockout.MaxFailedAccessAttempts;
                config.Lockout.AllowedForNewUsers = option.Lockout.AllowedForNewUsers;

                config.User.AllowedUserNameCharacters = option.User.AllowedUserNameCharacters;
                config.User.RequireUniqueEmail = option.User.RequireUniqueEmail;

                config.SignIn.RequireConfirmedEmail = option.SignIn.RequireConfirmedEmail;
                config.SignIn.RequireConfirmedPhoneNumber = option
                    .SignIn
                    .RequireConfirmedPhoneNumber;
            })
            .AddEntityFrameworkStores<BookShopContext>()
            .AddDefaultTokenProviders();
    }
}
