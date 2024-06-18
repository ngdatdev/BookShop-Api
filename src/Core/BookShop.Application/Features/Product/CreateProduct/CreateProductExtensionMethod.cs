namespace BookShop.Application.Features.Product.CreateProduct;

/// <summary>
///     Extension Method for CreateProduct features.
/// </summary>
public static class CreateProductExtensionMethod
{
    public static string ToAppCode(this CreateProductResponseStatusCode statusCode)
    {
        return $"{nameof(CreateProduct)}Feature: {statusCode}";
    }
}
