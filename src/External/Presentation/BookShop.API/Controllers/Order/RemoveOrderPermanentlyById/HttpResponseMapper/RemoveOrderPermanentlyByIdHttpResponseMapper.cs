namespace BookShop.API.Controllers.Order.RemoveOrderPermanentlyById.HttpResponseMapper;

/// <summary>
///     RemoveOrderPermanentlyById extension method
/// </summary>
internal static class RemoveOrderPermanentlyByIdHttpResponseMapper
{
    private static RemoveOrderPermanentlyByIdHttpResponseManager _RemoveOrderPermanentlyByIdHttpResponseManager;

    internal static RemoveOrderPermanentlyByIdHttpResponseManager Get()
    {
        return _RemoveOrderPermanentlyByIdHttpResponseManager ??= new();
    }
}
