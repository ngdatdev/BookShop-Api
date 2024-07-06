using BookShop.Application.Shared.Features;
using FluentValidation;

namespace BookShop.Application.Features.OrderDetails.RemoveOrderDetailTemporarilyById;

/// <summary>
///    RemoveOrderDetailTemporarilyById Request Validator
/// </summary>
public sealed class RemoveOrderDetailTemporarilyByIdRequestValidator
    : FeatureRequestValidator<
        RemoveOrderDetailTemporarilyByIdRequest,
        RemoveOrderDetailTemporarilyByIdResponse
    >
{
    public RemoveOrderDetailTemporarilyByIdRequestValidator()
    {
        RuleFor(request => request.OrderDetailId).NotEmpty();
    }
}
