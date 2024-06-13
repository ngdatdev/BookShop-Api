namespace BookShop.Configuration.Infrastructure.CacheRedis;

/// summary
///     The JwtAuthenticationOption class is used to hold connectionString redis configuration settings.
/// summary
public sealed class RedisOption
{
    public string ConnectionString { get; set; }
}
