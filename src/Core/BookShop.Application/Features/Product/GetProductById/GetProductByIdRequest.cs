using System;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Application.Features.Product.GetProductById;

/// <summary>
///     GetProductById Request
/// </summary>
public class GetProductByIdRequest : IFeatureRequest<GetProductByIdResponse>
{
    [FromRoute(Name = "product-id")]
    public Guid ProductId { get; set; }
}
