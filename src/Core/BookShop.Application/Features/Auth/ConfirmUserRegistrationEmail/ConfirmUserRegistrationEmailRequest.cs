using BookShop.Application.Shared.Features;

namespace BookShop.Application.Features.Auth.ConfirmUserRegistrationEmail;

/// <summary>
///     ConfirmUserRegistrationEmail Request
/// </summary>
public class ConfirmUserRegistrationEmailRequest
    : IFeatureRequest<ConfirmUserRegistrationEmailResponse>
{
    public string UserRegistrationEmailConfirmedTokenAsBase64 { get; init; }
}
