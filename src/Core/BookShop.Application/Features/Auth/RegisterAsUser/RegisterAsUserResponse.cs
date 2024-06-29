using BookShop.Application.Shared.Features;

namespace BookShop.Application.Features.Auth.RegisterAsUser;

/// <summary>
///     RegisterAsUser Response
/// </summary>
public class RegisterAsUserResponse : IFeatureResponse
{
    public RegisterAsUserResponseStatusCode StatusCode { get; init; }
}
