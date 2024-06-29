namespace BookShop.Application.Features.Auth.RefreshAccessToken;

/// <summary>
///     Extension Method for RefreshAccessToken features.
/// </summary>
public static class RefreshAccessTokenExtensionMethod
{
    public static string ToAppCode(this RefreshAccessTokenResponseStatusCode statusCode)
    {
        return $"{nameof(RefreshAccessToken)}Feature: {statusCode}";
    }
}
