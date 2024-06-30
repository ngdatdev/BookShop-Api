namespace BookShop.Application.Features.Auth.ResendUserRegistrationConfirmedEmail;

/// <summary>
///     Extension Method for ResendUserRegistrationConfirmedEmail features.
/// </summary>
public static class ResendUserRegistrationConfirmedEmailExtensionMethod
{
    public static string ToAppCode(
        this ResendUserRegistrationConfirmedEmailResponseStatusCode statusCode
    )
    {
        return $"{nameof(ResendUserRegistrationConfirmedEmail)}Feature: {statusCode}";
    }
}
