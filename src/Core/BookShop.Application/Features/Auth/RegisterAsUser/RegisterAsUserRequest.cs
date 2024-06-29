using BookShop.Application.Shared.Features;

namespace BookShop.Application.Features.Auth.RegisterAsUser;

/// <summary>
///     RegisterAsUser Request
/// </summary>
public class RegisterAsUserRequest : IFeatureRequest<RegisterAsUserResponse>
{
    public string Email { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}
