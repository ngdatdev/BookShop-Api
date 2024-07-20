namespace BookShop.Application.Features.Product.SearchProductsByKeyword;

/// <summary>
///     Extension Method for SearchProductsByKeyword features.
/// </summary>
public static class SearchProductsByKeywordExtensionMethod
{
    public static string ToAppCode(this SearchProductsByKeywordResponseStatusCode statusCode)
    {
        return $"{nameof(SearchProductsByKeyword)}Feature: {statusCode}";
    }
}
