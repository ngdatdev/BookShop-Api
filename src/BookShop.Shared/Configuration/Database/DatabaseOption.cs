namespace BookShop.Shared.Configuration.Database;

// The DatabaseOption class is used to hold various database configuration settings.
public class DatabaseOption
{
    // ConnectionString property to store the database connection string.
    public string ConnectionString { get; set; }

    // CommandTimeOut property to specify the command timeout duration (in seconds).
    public int CommandTimeOut { get; set; }

    // EnableRetryOnFailure property to specify if retries should be enabled on command failures.
    public int EnableRetryOnFailure { get; set; }

    // EnableSensitiveDataLogging property to indicate if sensitive data logging is enabled.
    public bool EnableSensitiveDataLogging { get; set; }

    // EnableDetailedErrors property to indicate if detailed errors are enabled.
    public bool EnableDetailedErrors { get; set; }

    // EnableThreadSafetyChecks property to indicate if thread safety checks are enabled.
    public bool EnableThreadSafetyChecks { get; set; }

    // EnableServiceProviderCaching property to indicate if service provider caching is enabled.
    public bool EnableServiceProviderCaching { get; set; }
}
