namespace BookShop.API.Controllers.Role.GetAllRolesTemporarilyRemoved.HttpResponseMapper;

/// <summary>
///     GetAllRolesTemporarilyRemoved extension method
/// </summary>
internal static class GetAllRolesTemporarilyRemovedHttpResponseMapper
{
    private static GetAllRolesTemporarilyRemovedHttpResponseManager _GetAllRolesTemporarilyRemovedHttpResponseManager;

    internal static GetAllRolesTemporarilyRemovedHttpResponseManager Get()
    {
        return _GetAllRolesTemporarilyRemovedHttpResponseManager ??= new();
    }
}
