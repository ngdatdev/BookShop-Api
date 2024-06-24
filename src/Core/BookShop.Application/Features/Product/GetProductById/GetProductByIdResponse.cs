using System.Collections;
using System.Collections.Generic;
using BookShop.Application.Shared.Features;
using BookShop.Application.Shared.Pagination;

namespace BookShop.Application.Features.Product.GetProductById;

/// <summary>
///     GetProductById Response
/// </summary>
public class GetProductByIdResponse : IFeatureResponse
{
    public GetProductByIdResponseStatusCode StatusCode { get; init; }

    public Body ResponseBody { get; init; }

    public sealed class Body
    {
        public Product ProductInfo { get; init; }

        public sealed class Product
        {
            public string FullName { get; init; }

            public string OriginalPrice { get; init; }

            public string SalePrice { get; init; }

            public string Discount { get; init; }

            public string ImageUrl { get; init; }

            public string Author { get; init; }

            public int NumberOfPage { get; init; }

            public int QuantityCurrent { get; init; }

            public int QuantitySold { get; init; }

            public string Publisher { get; init; }

            public string Languages { get; init; }

            public IEnumerable<string> SubUrls { get; init; }
        }
    }
}
