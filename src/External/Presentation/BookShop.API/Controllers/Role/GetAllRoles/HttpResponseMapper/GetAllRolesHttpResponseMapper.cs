namespace BookShop.API.Controllers.Role.GetAllRoles.HttpResponseMapper;

/// <summary>
///     GetAllRoles extension method
/// </summary>
internal static class GetAllRolesHttpResponseMapper
{
    private static GetAllRolesHttpResponseManager _GetAllRolesHttpResponseManager;

    internal static GetAllRolesHttpResponseManager Get()
    {
        return _GetAllRolesHttpResponseManager ??= new();
    }
}
