using BookShop.Application.Shared.Features;
using BookShop.Application.Shared.Pagination;

namespace BookShop.Application.Features.Product.GetProductsByCategoryId;

/// <summary>
///     GetProductsByCategoryId Response
/// </summary>
public class GetProductsByCategoryIdResponse : IFeatureResponse
{
    public GetProductsByCategoryIdResponseStatusCode StatusCode { get; init; }

    public Body ResponseBody { get; init; }

    public sealed class Body
    {
        public PaginationResponse<Product> Products { get; init; }

        public Category CategoryInfo { get; init; }

        public sealed class Product
        {
            public string FullName { get; init; }

            public string OriginalPrice { get; init; }

            public string SalePrice { get; init; }

            public string Discount { get; init; }

            public string ImageUrl { get; init; }

            public string Author { get; init; }
        }

        public sealed class Category
        {
            public string Name { get; init; }
            public string Description { get; init; }
        }
    }
}
