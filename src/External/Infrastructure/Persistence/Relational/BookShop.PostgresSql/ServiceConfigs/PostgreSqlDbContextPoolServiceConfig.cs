using System.Reflection;
using BookShop.Configuration.Infrastructure.Database;
using BookShop.PostgresSql.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookShop.PostgresSql.ServiceConfigs;

/// <summary>
///    SqlServer DbContext Pool service config.
/// </summary>
internal static class PostgreSqlDbContextPoolServiceConfig
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
    internal static void ConfigurePostgreSqlDbContextPool(
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
                    .UseNpgsql(
                        connectionString: baseDatabaseOption?.ConnectionString,
                        npgsqlOptionsAction: databaseOptionsAction =>
                        {
                            databaseOptionsAction
                                .CommandTimeout(commandTimeout: baseDatabaseOption?.CommandTimeOut)
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
