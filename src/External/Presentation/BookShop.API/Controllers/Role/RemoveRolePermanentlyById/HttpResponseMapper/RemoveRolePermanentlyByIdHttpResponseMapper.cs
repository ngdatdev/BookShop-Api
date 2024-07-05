namespace BookShop.API.Controllers.Role.RemoveRolePermanentlyById.HttpResponseMapper;

/// <summary>
///     RemoveRolePermanentlyById extension method
/// </summary>
internal static class RemoveRolePermanentlyByIdHttpResponseMapper
{
    private static RemoveRolePermanentlyByIdHttpResponseManager _RemoveRolePermanentlyByIdHttpResponseManager;

    internal static RemoveRolePermanentlyByIdHttpResponseManager Get()
    {
        return _RemoveRolePermanentlyByIdHttpResponseManager ??= new();
    }
}
