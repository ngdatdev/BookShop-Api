namespace BookShop.Application.Features.Auth.ForgotPassword;

/// <summary>
///     Extension Method for forgot password features.
/// </summary>
public static class ExtensionMethod
{
    public static string ToAppCode(this ForgotPasswordResponseStatusCode statusCode)
    {
        return $"{nameof(ForgotPassword)}Feature: {statusCode}";
    }
}
