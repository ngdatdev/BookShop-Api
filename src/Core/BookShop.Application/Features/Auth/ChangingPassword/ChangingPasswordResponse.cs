using BookShop.Application.Shared.Features;

namespace BookShop.Application.Features.Auth.ChangingPassword;

/// <summary>
///     ChangingPassword Response
/// </summary>
public class ChangingPasswordResponse : IFeatureResponse
{
    public ChangingPasswordResponseStatusCode StatusCode { get; init; }
}
