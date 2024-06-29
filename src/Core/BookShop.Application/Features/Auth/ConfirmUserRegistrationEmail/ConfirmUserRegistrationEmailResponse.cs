using BookShop.Application.Shared.Features;

namespace BookShop.Application.Features.Auth.ConfirmUserRegistrationEmail;

/// <summary>
///     ConfirmUserRegistrationEmail Response
/// </summary>
public sealed class ConfirmUserRegistrationEmailResponse : IFeatureResponse
{
    public ConfirmUserRegistrationEmailResponseStatusCode StatusCode { get; init; }

    public string ResponseBodyAsHtml { get; init; }
}
