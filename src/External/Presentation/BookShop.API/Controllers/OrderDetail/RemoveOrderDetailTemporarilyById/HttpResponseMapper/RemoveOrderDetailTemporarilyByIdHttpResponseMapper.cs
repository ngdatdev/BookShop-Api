namespace BookShop.API.Controllers.OrderDetail.RemoveOrderDetailTemporarilyById.HttpResponseMapper;

/// <summary>
///     RemoveOrderDetailTemporarilyById extension method
/// </summary>
internal static class RemoveOrderDetailTemporarilyByIdHttpResponseMapper
{
    private static RemoveOrderDetailTemporarilyByIdHttpResponseManager _RemoveOrderDetailTemporarilyByIdHttpResponseManager;

    internal static RemoveOrderDetailTemporarilyByIdHttpResponseManager Get()
    {
        return _RemoveOrderDetailTemporarilyByIdHttpResponseManager ??= new();
    }
}
