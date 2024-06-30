using BookShop.Application.Shared.Features;

namespace BookShop.Application.Features.Auth.ResendUserRegistrationConfirmedEmail;

/// <summary>
///     ResendUserRegistrationConfirmedEmail Request
/// </summary>
public class ResendUserRegistrationConfirmedEmailRequest
    : IFeatureRequest<ResendUserRegistrationConfirmedEmailResponse>
{
    public string Username { get; set; }
}
