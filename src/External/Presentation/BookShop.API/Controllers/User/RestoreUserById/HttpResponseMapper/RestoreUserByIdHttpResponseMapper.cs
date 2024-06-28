namespace BookShop.API.Controllers.User.RestoreUserById.HttpResponseMapper;

/// <summary>
///     RestoreUserById extension method
/// </summary>
internal static class RestoreUserByIdHttpResponseMapper
{
    private static RestoreUserByIdHttpResponseManager _RestoreUserByIddHttpResponseManager;

    internal static RestoreUserByIdHttpResponseManager Get()
    {
        return _RestoreUserByIddHttpResponseManager ??= new();
    }
}
