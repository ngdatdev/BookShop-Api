namespace BookShop.API.Controllers.User.RemoveUserPermanentlyById.HttpResponseMapper;

/// <summary>
///     RemoveUserPermanentlyById extension method
/// </summary>
internal static class RemoveUserPermanentlyByIdHttpResponseMapper
{
    private static RemoveUserPermanentlyByIdHttpResponseManager _RemoveUserPermanentlyByIdHttpResponseManager;

    internal static RemoveUserPermanentlyByIdHttpResponseManager Get()
    {
        return _RemoveUserPermanentlyByIdHttpResponseManager ??= new();
    }
}
