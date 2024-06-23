using System;
using System.IO;
using System.Linq;
using BookShop.Application.Shared.Features;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace BookShop.Application.Features.Product.RemoveProductTemporarilyById;

/// <summary>
///    RemoveProductTemporarilyById Request Validator
/// </summary>
public sealed class RemoveProductTemporarilyByIdRequestValidator
    : FeatureRequestValidator<
        RemoveProductTemporarilyByIdRequest,
        RemoveProductTemporarilyByIdResponse
    >
{
    public RemoveProductTemporarilyByIdRequestValidator()
    {
        RuleFor(request => request.ProductId).NotEmpty();
    }
}
