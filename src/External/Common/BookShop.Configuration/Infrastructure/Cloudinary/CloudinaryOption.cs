
namespace BookShop.Configuration.Infrastructure.Cloudinary;

/// summary
///     The JwtAuthenticationOption class is used to hold connectionString redis configuration settings.
/// summary
public class CloudinaryOption
    {
        public string CloudName { get; set; }
        public string ApiKey { get; set; }
        public string ApiSecret { get; set; }
    }

