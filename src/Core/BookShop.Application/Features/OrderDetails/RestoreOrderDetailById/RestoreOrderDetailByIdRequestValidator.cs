using BookShop.Application.Shared.Features;
using FluentValidation;

namespace BookShop.Application.Features.OrderDetails.RestoreOrderDetailById;

/// <summary>
///    RestoreOrderDetailById Request Validator
/// </summary>
public sealed class RestoreOrderDetailByIdRequestValidator
    : FeatureRequestValidator<RestoreOrderDetailByIdRequest, RestoreOrderDetailByIdResponse>
{
    public RestoreOrderDetailByIdRequestValidator()
    {
        RuleFor(request => request.OrderDetailId).NotEmpty();
    }
}
