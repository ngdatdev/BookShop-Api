namespace BookShop.Application.Features.Product.GetAllTemporarilyRemovedProducts;

/// <summary>
///     Extension Method for GetAllTemporarilyRemovedProducts features.
/// </summary>
public static class GetAllTemporarilyRemovedProductsExtensionMethod
{
    public static string ToAppCode(
        this GetAllTemporarilyRemovedProductsResponseStatusCode statusCode
    )
    {
        return $"{nameof(GetAllTemporarilyRemovedProducts)}Feature: {statusCode}";
    }
}
