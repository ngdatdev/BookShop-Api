using BookShop.Application.Shared.Features;
using FluentValidation;

namespace BookShop.Application.Features.OrderDetails.SwitchOrderStatusToCancel;

/// <summary>
///    SwitchOrderStatusToCancel Request Validator
/// </summary>
public sealed class SwitchOrderStatusToCancelRequestValidator
    : FeatureRequestValidator<SwitchOrderStatusToCancelRequest, SwitchOrderStatusToCancelResponse>
{
    public SwitchOrderStatusToCancelRequestValidator()
    {
        RuleFor(request => request.OrderDetailId).NotEmpty();
    }
}
