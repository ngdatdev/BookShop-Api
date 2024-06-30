namespace BookShop.API.Controllers.User.UpdateUserById.HttpResponseMapper;

/// <summary>
///     UpdateUserById extension method
/// </summary>
internal static class UpdateUserByIdHttpResponseMapper
{
    private static UpdateUserByIdHttpResponseManager _UpdateUserByIdHttpResponseManager;

    internal static UpdateUserByIdHttpResponseManager Get()
    {
        return _UpdateUserByIdHttpResponseManager ??= new();
    }
}
