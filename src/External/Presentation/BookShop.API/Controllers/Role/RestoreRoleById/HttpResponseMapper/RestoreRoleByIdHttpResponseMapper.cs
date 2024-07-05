namespace BookShop.API.Controllers.Role.RestoreRoleById.HttpResponseMapper;

/// <summary>
///     RestoreRoleById extension method
/// </summary>
internal static class RestoreRoleByIdHttpResponseMapper
{
    private static RestoreRoleByIdHttpResponseManager _RestoreRoleByIdHttpResponseManager;

    internal static RestoreRoleByIdHttpResponseManager Get()
    {
        return _RestoreRoleByIdHttpResponseManager ??= new();
    }
}
