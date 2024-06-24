using BookShop.Application.Shared.Features;
using FluentValidation;

namespace BookShop.Application.Features.Product.RemoveProductPermanentlyById;

/// <summary>
///    RemoveProductPermanentlyById Request Validator
/// </summary>
public sealed class RemoveProductPermanentlyByIdRequestValidator
    : FeatureRequestValidator<
        RemoveProductPermanentlyByIdRequest,
        RemoveProductPermanentlyByIdResponse
    >
{
    public RemoveProductPermanentlyByIdRequestValidator()
    {
        RuleFor(request => request.ProductId).NotEmpty();
    }
}
