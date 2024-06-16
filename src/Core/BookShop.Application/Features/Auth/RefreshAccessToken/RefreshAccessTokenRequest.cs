using BookShop.Application.Shared.Features;

namespace BookShop.Application.Features.Auth.RefreshAccessToken;

/// <summary>
///     RefreshAccessToken Request
/// </summary>
public class RefreshAccessTokenRequest : IFeatureRequest<RefreshAccessTokenResponse>
{
    public string RefreshToken { get; set; }
}
