namespace BookShop.API.Controllers.Product.GetAllTemporarilyRemovedProducts.HttpResponseMapper;

/// <summary>
///     GetAllTemporarilyRemovedProducts extension method
/// </summary>
internal static class GetAllTemporarilyRemovedProductsHttpResponseMapper
{
    private static GetAllTemporarilyRemovedProductsHttpResponseManager _GetAllTemporarilyRemovedProductsHttpResponseManager;

    internal static GetAllTemporarilyRemovedProductsHttpResponseManager Get()
    {
        return _GetAllTemporarilyRemovedProductsHttpResponseManager ??= new();
    }
}
