using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.Configuration.Presentation.WebApi.Authentication;

/// summary
///     The GoogleAuthenticationOption class is used to hold various google authentication configuration settings.
/// summary
public class GoogleAuthenticationOption
{
    public GoogleOption Google { get; set; }

    public sealed class GoogleOption
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
}
