namespace BookShop.API.Controllers.Product.CreateProductEndpoint.HttpResponseMapper;

/// <summary>
///     CreateProduct extension method
/// </summary>
internal static class CreateProductHttpResponseMapper
{
    private static CreateProductHttpResponseManager _CreateProductHttpResponseManager;

    internal static CreateProductHttpResponseManager Get()
    {
        return _CreateProductHttpResponseManager ??= new();
    }
}
