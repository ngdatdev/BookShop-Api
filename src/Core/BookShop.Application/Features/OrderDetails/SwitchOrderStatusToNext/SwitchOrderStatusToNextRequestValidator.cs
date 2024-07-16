using BookShop.Application.Shared.Features;
using FluentValidation;

namespace BookShop.Application.Features.OrderDetails.SwitchOrderStatusToNext;

/// <summary>
///    SwitchOrderStatusToNext Request Validator
/// </summary>
public sealed class SwitchOrderStatusToNextRequestValidator
    : FeatureRequestValidator<
        SwitchOrderStatusToNextRequest,
        SwitchOrderStatusToNextResponse
    >
{
    public SwitchOrderStatusToNextRequestValidator()
    {
        RuleFor(request => request.OrderDetailId).NotEmpty();
    }
}
