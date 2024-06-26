using BookShop.Application.Shared.Features;
using BookShop.Application.Shared.Pagination;

namespace BookShop.Application.Features.Product.GetAllProducts;

/// <summary>
///     GetAllProducts Response
/// </summary>
public class GetAllProductsResponse : IFeatureResponse
{
    public GetAllProductsResponseStatusCode StatusCode { get; init; }

    public Body ResponseBody { get; init; }

    public sealed class Body
    {
        public PaginationResponse<Product> Products { get; init; }

        public sealed class Product
        {
            public string FullName { get; init; }

            public string OriginalPrice { get; init; }

            public string SalePrice { get; init; }

            public string Discount { get; init; }

            public string ImageUrl { get; init; }

            public string Author { get; init; }
        }
    }
}
