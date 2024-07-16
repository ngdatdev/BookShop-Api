using BookShop.Application.Shared.Features;
using FluentValidation;

namespace BookShop.Application.Features.OrderDetails.RestoreOrderStatusToConfirm;

/// <summary>
///    RestoreOrderStatusToConfirm Request Validator
/// </summary>
public sealed class RestoreOrderStatusToConfirmRequestValidator
    : FeatureRequestValidator<
        RestoreOrderStatusToConfirmRequest,
        RestoreOrderStatusToConfirmResponse
    >
{
    public RestoreOrderStatusToConfirmRequestValidator()
    {
        RuleFor(request => request.OrderDetailId).NotEmpty();
    }
}
