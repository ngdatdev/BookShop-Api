using BookShop.Application.Shared.Features;

namespace BookShop.Application.Features.Auth.ForgotPassword;

/// <summary>
///     ForgotPassword Request
/// </summary>
public class ForgotPasswordRequest : IFeatureRequest<ForgotPasswordResponse>
{
    public string Email { get; init; }
}
