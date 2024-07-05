namespace BookShop.API.Controllers.Role.RemoveRoleTemporarilyById.HttpResponseMapper;

/// <summary>
///     RemoveRoleTemporarilyById extension method
/// </summary>
internal static class RemoveRoleTemporarilyByIdHttpResponseMapper
{
    private static RemoveRoleTemporarilyByIdHttpResponseManager _RemoveRoleTemporarilyByIdHttpResponseManager;

    internal static RemoveRoleTemporarilyByIdHttpResponseManager Get()
    {
        return _RemoveRoleTemporarilyByIdHttpResponseManager ??= new();
    }
}
