using BookShop.Application.Shared.Features;

namespace BookShop.Application.Features.Auth.Login;

/// <summary>
///     Login Request
/// </summary>
public class LoginRequest : IFeatureRequest<LoginResponse>
{
    public string Username { get; init; }
    public string Password { get; init; }
    public bool IsRemember { get; set; }
}
