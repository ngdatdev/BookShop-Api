using System.Reflection;
using BookShop.DataAccess.Data;
using BookShop.Shared.Configuration.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookShop.DataAccess.ServiceConfigs;

/// <summary>
///    SqlServer DbContext Pool service config.
/// </summary>
internal static class SqlServerDbContextPoolServiceConfig
{
    /// <summary>
    ///     Configure the db context pool service.
    /// </summary>
    /// <param name="services">
    ///     Service container.
    /// </param>
    /// <param name="configuration">
    ///     Load configuration for configuration
    ///     file (appsetting).
    /// </param>
    internal static void ConfigureSqlServerDbContextPool(
        this IServiceCollection services,
        IConfigurationManager configuration
    )
    {
        services.AddDbContext<BookShopContext>(
            optionsAction: (provider, config) =>
            {
                var baseDatabaseOption = configuration
                    .GetRequiredSection("Database")
                    .GetRequiredSection("BookShop")
                    .Get<DatabaseOption>();

                config
                    .UseSqlServer(
                        connectionString: baseDatabaseOption.ConnectionString,
                        sqlServerOptionsAction: databaseOptionsAction =>
                        {
                            databaseOptionsAction
                                .CommandTimeout(commandTimeout: baseDatabaseOption.CommandTimeOut)
                                .EnableRetryOnFailure(
                                    maxRetryCount: baseDatabaseOption.EnableRetryOnFailure
                                )
                                .MigrationsAssembly(
                                    assemblyName: Assembly.GetExecutingAssembly().GetName().Name
                                );
                        }
                    )
                    .EnableSensitiveDataLogging(
                        sensitiveDataLoggingEnabled: baseDatabaseOption.EnableSensitiveDataLogging
                    )
                    .EnableDetailedErrors(
                        detailedErrorsEnabled: baseDatabaseOption.EnableDetailedErrors
                    )
                    .EnableThreadSafetyChecks(
                        enableChecks: baseDatabaseOption.EnableThreadSafetyChecks
                    )
                    .EnableServiceProviderCaching(
                        cacheServiceProvider: baseDatabaseOption.EnableServiceProviderCaching
                    );
            }
        );
    }
}
