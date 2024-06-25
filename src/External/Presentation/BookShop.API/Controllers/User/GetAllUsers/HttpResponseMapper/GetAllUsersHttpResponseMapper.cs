namespace BookShop.API.Controllers.User.GetAllUsers.HttpResponseMapper;

/// <summary>
///     GetAllUsers extension method
/// </summary>
internal static class GetAllUsersHttpResponseMapper
{
    private static GetAllUsersHttpResponseManager _GetAllUsersHttpResponseManager;

    internal static GetAllUsersHttpResponseManager Get()
    {
        return _GetAllUsersHttpResponseManager ??= new();
    }
}
