namespace BookShop.Application.Features.Carts.GetCartByUserId;

/// <summary>
///     Extension Method for GetCartByUserId features.
/// </summary>
public static class GetCartByUserIdExtensionMethod
{
    public static string ToAppCode(this GetCartByUserIdResponseStatusCode statusCode)
    {
        return $"{nameof(GetCartByUserId)}Feature: {statusCode}";
    }
}
