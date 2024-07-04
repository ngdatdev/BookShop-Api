using BookShop.Application.Shared.Features;
using FluentValidation;

namespace BookShop.Application.Features.Orders.RemoveOrderPermanentlyById;

/// <summary>
///    RemoveOrderPermanentlyById Request Validator
/// </summary>
public sealed class RemoveOrderPermanentlyByIdRequestValidator
    : FeatureRequestValidator<RemoveOrderPermanentlyByIdRequest, RemoveOrderPermanentlyByIdResponse>
{
    public RemoveOrderPermanentlyByIdRequestValidator()
    {
        RuleFor(request => request.OrderId).NotEmpty();
    }
}
