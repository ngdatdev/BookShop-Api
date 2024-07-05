namespace BookShop.API.Controllers.Role.UpdateRoleById.HttpResponseMapper;

/// <summary>
///     UpdateRoleById extension method
/// </summary>
internal static class UpdateRoleByIdHttpResponseMapper
{
    private static UpdateRoleByIdHttpResponseManager _UpdateRoleByIdHttpResponseManager;

    internal static UpdateRoleByIdHttpResponseManager Get()
    {
        return _UpdateRoleByIdHttpResponseManager ??= new();
    }
}
