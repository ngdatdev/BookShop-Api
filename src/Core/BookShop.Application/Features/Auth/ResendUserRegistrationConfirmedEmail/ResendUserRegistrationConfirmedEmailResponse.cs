using BookShop.Application.Shared.Features;

namespace BookShop.Application.Features.Auth.ResendUserRegistrationConfirmedEmail;

/// <summary>
///     ResendUserRegistrationConfirmedEmail Response
/// </summary>
public class ResendUserRegistrationConfirmedEmailResponse : IFeatureResponse
{
    public ResendUserRegistrationConfirmedEmailResponseStatusCode StatusCode { get; init; }
}
