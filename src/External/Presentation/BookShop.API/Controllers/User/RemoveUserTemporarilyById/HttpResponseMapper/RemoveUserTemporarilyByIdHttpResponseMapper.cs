namespace BookShop.API.Controllers.User.RemoveUserTemporarilyById.HttpResponseMapper;

/// <summary>
///     RemoveUserTemporarilyById extension method
/// </summary>
internal static class RemoveUserTemporarilyByIdHttpResponseMapper
{
    private static RemoveUserTemporarilyByIdHttpResponseManager _RemoveUserTemporarilyByIdHttpResponseManager;

    internal static RemoveUserTemporarilyByIdHttpResponseManager Get()
    {
        return _RemoveUserTemporarilyByIdHttpResponseManager ??= new();
    }
}
