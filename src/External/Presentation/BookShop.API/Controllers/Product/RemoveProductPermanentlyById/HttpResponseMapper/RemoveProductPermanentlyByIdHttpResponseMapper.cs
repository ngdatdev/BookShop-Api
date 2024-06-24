namespace BookShop.API.Controllers.Product.RemoveProductPermanentlyByIdEndpoint.HttpResponseMapper;

/// <summary>
///     RemoveProductPermanentlyById extension method
/// </summary>
internal static class RemoveProductPermanentlyByIdHttpResponseMapper
{
    private static RemoveProductPermanentlyByIdHttpResponseManager _RemoveProductPermanentlyByIdHttpResponseManager;

    internal static RemoveProductPermanentlyByIdHttpResponseManager Get()
    {
        return _RemoveProductPermanentlyByIdHttpResponseManager ??= new();
    }
}
