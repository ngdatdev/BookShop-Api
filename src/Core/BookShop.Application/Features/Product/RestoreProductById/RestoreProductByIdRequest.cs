using System;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Application.Features.Product.RestoreProductById;

/// <summary>
///     RestoreProductById Request
/// </summary>
public class RestoreProductByIdRequest : IFeatureRequest<RestoreProductByIdResponse>
{
    [FromRoute(Name = "product-id")]
    public Guid ProductId { get; init; }
}
