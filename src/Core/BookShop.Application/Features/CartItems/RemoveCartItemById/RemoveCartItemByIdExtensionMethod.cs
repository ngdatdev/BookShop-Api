namespace BookShop.Application.Features.CartItems.RemoveCartItemById;

/// <summary>
///     Extension Method for RemoveCartItemById features.
/// </summary>
public static class RemoveCartItemByIdExtensionMethod
{
    public static string ToAppCode(this RemoveCartItemByIdResponseStatusCode statusCode)
    {
        return $"{nameof(RemoveCartItemById)}Feature: {statusCode}";
    }
}
