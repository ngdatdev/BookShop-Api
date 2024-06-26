using System;
using System.Collections.Generic;
using BookShop.Application.Features.Product.UpdateProductById;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Http;

namespace BookShop.Application.Features.Product.UpdateProductById;

/// <summary>
///     UpdateProductById Request
/// </summary>
public class UpdateProductByIdRequest : IFeatureRequest<UpdateProductByIdResponse>
{
    public Guid ProductId { get; init; }

    public string FullName { get; init; }

    public string Description { get; init; }

    public decimal Price { get; init; }

    public int Discount { get; init; }

    public string Size { get; init; }

    public int NumberOfPage { get; init; }

    public int QuantityCurrent { get; init; }

    public IFormFile MainUrl { get; init; }

    public string OldMainUrl { get; init; }

    public string Author { get; init; }

    public string Publisher { get; init; }

    public string Languages { get; init; }

    public IEnumerable<Guid> CategoriesId { get; init; }

    public IEnumerable<IFormFile> SubImages { get; init; }

    public IEnumerable<string> OldSubUrls { get; init; }
}
