using BookShop.Application.Shared.Features;
using FluentValidation;

namespace BookShop.Application.Features.OrderDetails.RemoveOrderDetailPermanentlyById;

/// <summary>
///    RemoveOrderDetailPermanentlyById Request Validator
/// </summary>
public sealed class RemoveOrderDetailPermanentlyByIdRequestValidator
    : FeatureRequestValidator<
        RemoveOrderDetailPermanentlyByIdRequest,
        RemoveOrderDetailPermanentlyByIdResponse
    >
{
    public RemoveOrderDetailPermanentlyByIdRequestValidator()
    {
        RuleFor(request => request.OrderDetailId).NotEmpty();
    }
}
