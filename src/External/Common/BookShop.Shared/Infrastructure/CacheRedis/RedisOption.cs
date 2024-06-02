namespace BookShop.Configuration.Infrastructure.CacheRedis;

// The JwtAuthenticationOption class is used to hold connectionString redis configuration settings.
public sealed class RedisOption
{
    public string ConnectionString { get; set; }
}
