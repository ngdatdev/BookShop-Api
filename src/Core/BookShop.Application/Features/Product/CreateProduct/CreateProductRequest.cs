using System.Collections.Generic;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Http;

namespace BookShop.Application.Features.Product.CreateProduct;

/// <summary>
///     CreateProduct Request
/// </summary>
public class CreateProductRequest : IFeatureRequest<CreateProductResponse>
{
    public string FullName { get; init; }

    public string Description { get; init; }

    public decimal Price { get; init; }

    public int Discount { get; init; }

    public string Size { get; init; }

    public int NumberOfPage { get; init; }

    public int QuantityCurrent { get; init; }

    public IFormFile ImageUrl { get; init; }

    public string Author { get; init; }

    public string Publisher { get; init; }

    public string Languages { get; init; }

    public IEnumerable<IFormFile> SubImages { get; init; }
}
