namespace BookShop.API.Controllers.Auth.LoginByGoogle.HttpResponseMapper;

/// <summary>
///     LoginByGoogle extension method
/// </summary>
internal static class LoginByGoogleHttpResponseMapper
{
    private static LoginByGoogleHttpResponseManager _LoginByGoogleHttpResponseManager;

    internal static LoginByGoogleHttpResponseManager Get()
    {
        return _LoginByGoogleHttpResponseManager ??= new();
    }
}
