namespace BookShop.Application.Features.CartItems.CreateOrder;

/// <summary>
///     Extension Method for CreateOrder features.
/// </summary>
public static class CreateOrderExtensionMethod
{
    public static string ToAppCode(this CreateOrderResponseStatusCode statusCode)
    {
        return $"{nameof(CreateOrder)}Feature: {statusCode}";
    }
}
