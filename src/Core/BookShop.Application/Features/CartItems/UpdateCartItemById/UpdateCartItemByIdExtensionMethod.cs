namespace BookShop.Application.Features.CartItems.UpdateCartItemById;

/// <summary>
///     Extension Method for UpdateCartItemById features.
/// </summary>
public static class UpdateCartItemByIdExtensionMethod
{
    public static string ToAppCode(this UpdateCartItemByIdResponseStatusCode statusCode)
    {
        return $"{nameof(UpdateCartItemById)}Feature: {statusCode}";
    }
}
