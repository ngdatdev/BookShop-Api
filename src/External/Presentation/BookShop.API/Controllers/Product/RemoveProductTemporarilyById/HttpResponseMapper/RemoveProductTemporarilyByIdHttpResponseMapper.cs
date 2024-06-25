namespace BookShop.API.Controllers.Product.RemoveProductTemporarilyById.HttpResponseMapper;

/// <summary>
///     RemoveProductTemporarilyById extension method
/// </summary>
internal static class RemoveProductTemporarilyByIdHttpResponseMapper
{
    private static RemoveProductTemporarilyByIdHttpResponseManager _RemoveProductTemporarilyByIdHttpResponseManager;

    internal static RemoveProductTemporarilyByIdHttpResponseManager Get()
    {
        return _RemoveProductTemporarilyByIdHttpResponseManager ??= new();
    }
}
