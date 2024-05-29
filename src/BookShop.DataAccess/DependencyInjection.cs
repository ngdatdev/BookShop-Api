using System;
using System.Reflection;
using BookShop.DataAccess.Data;
using BookShop.DataAccess.Entities;
using BookShop.DataAccess.ServiceConfigs;
using BookShop.DataAccess.UnitOfWork;
using BookShop.Shared.Configuration.AspnetCoreIdentityOption;
using BookShop.Shared.Configuration.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookShop.DataAccess;

/// <summary>
///     Configure services for data access layer.
/// </summary>
public static class DependencyInjection
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
    public static void ConfigureSqlRelationalDatabase(
        this IServiceCollection services,
        IConfigurationManager configuration
    )
    {
        services.ConfigureSqlServerDbContextPool(configuration: configuration);
        services.ConfigureCore();
        services.ConfigureAspNetCoreIdentity(configuration: configuration);
    }
}
