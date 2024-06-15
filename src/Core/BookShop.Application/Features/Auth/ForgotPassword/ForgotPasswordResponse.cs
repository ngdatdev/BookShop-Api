using BookShop.Application.Shared.Features;

namespace BookShop.Application.Features.Auth.ForgotPassword;

/// <summary>
///     ForgotPassword Response
/// </summary>
public class ForgotPasswordResponse : IFeatureResponse
{
    public ForgotPasswordResponseStatusCode StatusCode { get; init; }
}
