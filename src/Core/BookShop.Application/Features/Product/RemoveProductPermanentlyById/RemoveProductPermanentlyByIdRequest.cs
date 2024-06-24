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
    public Guid ProductId { get; set; }
}
