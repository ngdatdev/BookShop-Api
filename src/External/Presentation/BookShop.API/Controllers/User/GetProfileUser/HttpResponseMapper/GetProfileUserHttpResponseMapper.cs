namespace BookShop.API.Controllers.User.GetProfileUser.HttpResponseMapper;

/// <summary>
///     GetProfileUser extension method
/// </summary>
internal static class GetProfileUserHttpResponseMapper
{
    private static GetProfileUserHttpResponseManager _GetProfileUserHttpResponseManager;

    internal static GetProfileUserHttpResponseManager Get()
    {
        return _GetProfileUserHttpResponseManager ??= new();
    }
}
