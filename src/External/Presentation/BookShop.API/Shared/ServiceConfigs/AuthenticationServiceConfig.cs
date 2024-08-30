using System.Security.Cryptography;
using System.Text;
using BookShop.Configuration.Presentation.WebApi.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace BookShop.API.Shared.ServiceConfigs;

/// <summary>
///     Authentication service config.
/// </summary>
internal static class AuthenticationServiceConfig
{
    /// <summary>
    ///     Entry to configuring multiple services.
    /// </summary>
    /// <param name="services">
    ///     Service container.
    /// </param>
    /// <param name="configuration">
    ///     Load configuration for configuration
    ///     file (appsetting).
    /// </param>
    internal static void ConfigAuthentication(
        this IServiceCollection services,
        IConfigurationManager configuration
    )
    {
        var option = configuration
            .GetRequiredSection(key: "Authentication")
            .Get<JwtAuthenticationOption>();

        var googleOption = configuration
            .GetRequiredSection(key: "Authentication")
            .GetRequiredSection(key: "OAuth2")
            .Get<GoogleAuthenticationOption>();

        TokenValidationParameters tokenValidationParameters =
            new()
            {
                ValidateIssuer = option.Jwt.ValidateIssuer,
                ValidateAudience = option.Jwt.ValidateAudience,
                ValidateLifetime = option.Jwt.ValidateLifetime,
                ValidateIssuerSigningKey = option.Jwt.ValidateIssuerSigningKey,
                RequireExpirationTime = option.Jwt.RequireExpirationTime,
                ValidTypes = option.Jwt.ValidTypes,
                ValidIssuer = option.Jwt.ValidIssuer,
                ValidAudience = option.Jwt.ValidAudience,
                IssuerSigningKey = new SymmetricSecurityKey(
                    key: new HMACSHA256(
                        key: Encoding.UTF8.GetBytes(s: option.Jwt.IssuerSigningKey)
                    ).Key
                )
            };

        services
            .AddSingleton(implementationInstance: option)
            .AddSingleton(implementationInstance: tokenValidationParameters)
            .AddAuthentication(configureOptions: config =>
            {
                config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                config.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
                config.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(configureOptions: config =>
                config.TokenValidationParameters = tokenValidationParameters
            )
            .AddGoogle(options =>
            {
                options.ClientId = googleOption.Google.ClientId;
                options.ClientSecret = googleOption.Google.ClientSecret;
                //options.CallbackPath = "/signin-google";
                //options.SaveTokens = true;
            });
        ;
    }
}
