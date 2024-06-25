using BookShop.Application.Shared.Features;
using FluentValidation;

namespace BookShop.Application.Features.Product.RestoreProductById;

/// <summary>
///    RestoreProductById Request Validator
/// </summary>
public sealed class RestoreProductByIdRequestValidator
    : FeatureRequestValidator<RestoreProductByIdRequest, RestoreProductByIdResponse>
{
    public RestoreProductByIdRequestValidator()
    {
        RuleFor(request => request.ProductId).NotEmpty();
    }
}
