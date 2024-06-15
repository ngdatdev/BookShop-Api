using BookShop.Application.Shared.Features;

namespace BookShop.Application.Features.Auth.ChangingPassword;

/// <summary>
///     ChangingPassword Request
/// </summary>
public class ChangingPasswordRequest : IFeatureRequest<ChangingPasswordResponse>
{
    public string NewPassword { get; init; }

    public string ResetPasswordToken { get; init; }
}
