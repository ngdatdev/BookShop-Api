namespace BookShop.API.Controllers.User.GetAllUsersTemporarilyRemovedById.HttpResponseMapper;

/// <summary>
///     GetAllUsersTemporarilyRemovedById extension method
/// </summary>
internal static class GetAllUsersTemporarilyRemovedByIdHttpResponseMapper
{
    private static GetAllUsersTemporarilyRemovedByIdHttpResponseManager _GetAllUsersTemporarilyRemovedByIdHttpResponseManager;

    internal static GetAllUsersTemporarilyRemovedByIdHttpResponseManager Get()
    {
        return _GetAllUsersTemporarilyRemovedByIdHttpResponseManager ??= new();
    }
}
