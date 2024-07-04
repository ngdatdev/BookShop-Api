namespace BookShop.API.Controllers.Order.RemoveOrderTemporarilyById.HttpResponseMapper;

/// <summary>
///     RemoveOrderTemporarilyById extension method
/// </summary>
internal static class RemoveOrderTemporarilyByIdHttpResponseMapper
{
    private static RemoveOrderTemporarilyByIdHttpResponseManager _RemoveOrderTemporarilyByIdHttpResponseManager;

    internal static RemoveOrderTemporarilyByIdHttpResponseManager Get()
    {
        return _RemoveOrderTemporarilyByIdHttpResponseManager ??= new();
    }
}
