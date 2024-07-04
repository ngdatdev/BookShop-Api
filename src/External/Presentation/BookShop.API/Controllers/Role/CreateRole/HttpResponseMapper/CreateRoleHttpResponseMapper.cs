namespace BookShop.API.Controllers.Role.CreateRole.HttpResponseMapper;

/// <summary>
///     CreateRole extension method
/// </summary>
internal static class CreateRoleHttpResponseMapper
{
    private static CreateRoleHttpResponseManager _CreateRoleHttpResponseManager;

    internal static CreateRoleHttpResponseManager Get()
    {
        return _CreateRoleHttpResponseManager ??= new();
    }
}
