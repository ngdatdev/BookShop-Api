using BookShop.Application.Shared.Features;

namespace BookShop.Application.Features.Auth.LoginByGoogle;

/// <summary>
///     LoginByGoogle Request
/// </summary>
public class LoginByGoogleRequest : IFeatureRequest<LoginByGoogleResponse>
{
    public string IdToken { get; init; }
}
