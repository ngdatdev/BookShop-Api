namespace BookShop.API.Controllers.Auth.ForgotPasswordEndpoint.HttpResponseMapper;

/// <summary>
///     ForgotPassword extension method
/// </summary>
internal static class ForgotPasswordHttpResponseMapper
{
    private static ForgotPasswordHttpResponseManager _ForgotPasswordHttpResponseManager;

    internal static ForgotPasswordHttpResponseManager Get()
    {
        return _ForgotPasswordHttpResponseManager ??= new();
    }
}
