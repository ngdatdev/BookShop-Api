using BookShop.Application.Shared.Features;

namespace BookShop.Application.Features.Auth.RefreshAccessToken;

/// <summary>
///     RefreshAccessToken Response
/// </summary>
public class RefreshAccessTokenResponse : IFeatureResponse
{
    public RefreshAccessTokenResponseStatusCode StatusCode { get; init; }

    public Body ResponseBody { get; init; }

    public sealed class Body
    {
        public string AccessToken { get; init; }

        public string RefreshToken { get; init; }
    }
}
