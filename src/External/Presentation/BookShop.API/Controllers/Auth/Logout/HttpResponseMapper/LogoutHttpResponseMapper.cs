namespace BookShop.API.Controllers.Auth.Logout.HttpResponseMapper;

/// <summary>
///     Logout extension method
/// </summary>
internal static class LogoutHttpResponseMapper
{
    private static LogoutHttpResponseManager _LogoutHttpResponseManager;

    internal static LogoutHttpResponseManager Get()
    {
        return _LogoutHttpResponseManager ??= new();
    }
}
