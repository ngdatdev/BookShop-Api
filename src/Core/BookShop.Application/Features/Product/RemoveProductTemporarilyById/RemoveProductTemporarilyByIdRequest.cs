using System;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Application.Features.Product.RemoveProductTemporarilyById;

/// <summary>
///     RemoveProductTemporarilyById Request
/// </summary>
public class RemoveProductTemporarilyByIdRequest
    : IFeatureRequest<RemoveProductTemporarilyByIdResponse>
{
    public Guid ProductId { get; init; }
}
