using System;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Application.Features.Product.RemoveProductPermanentlyById;

/// <summary>
///     RemoveProductPermanentlyById Request
/// </summary>
public class RemoveProductPermanentlyByIdRequest
    : IFeatureRequest<RemoveProductPermanentlyByIdResponse>
{
    [FromRoute(Name = "product-id")]
    public Guid ProductId { get; set; }
}
