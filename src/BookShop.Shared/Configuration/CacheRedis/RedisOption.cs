using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.Shared.Configuration.CacheRedis;

// The JwtAuthenticationOption class is used to hold connectionString redis configuration settings.
public sealed class RedisOption
{
    public string ConnectionString { get; set; }
}
