namespace BookShop.API.Controllers.Auth.LoginEndpoint.HttpResponseMapper;

/// <summary>
///     Login extension method
/// </summary>
internal static class LoginHttpResponseMapper
{
    private static LoginHttpResponseManager _LoginHttpResponseManager;

    internal static LoginHttpResponseManager Get()
    {
        return _LoginHttpResponseManager ??= new();
    }
}
