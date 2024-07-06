namespace BookShop.API.Controllers.OrderDetail.RemoveOrderDetailPermanentlyById.HttpResponseMapper;

/// <summary>
///     RemoveOrderDetailPermanentlyById extension method
/// </summary>
internal static class RemoveOrderDetailPermanentlyByIdHttpResponseMapper
{
    private static RemoveOrderDetailPermanentlyByIdHttpResponseManager _RemoveOrderDetailPermanentlyByIdHttpResponseManager;

    internal static RemoveOrderDetailPermanentlyByIdHttpResponseManager Get()
    {
        return _RemoveOrderDetailPermanentlyByIdHttpResponseManager ??= new();
    }
}
