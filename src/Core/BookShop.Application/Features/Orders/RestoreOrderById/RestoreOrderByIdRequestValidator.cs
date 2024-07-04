using BookShop.Application.Shared.Features;
using FluentValidation;

namespace BookShop.Application.Features.Orders.RestoreOrderById;

/// <summary>
///    RestoreOrderById Request Validator
/// </summary>
public sealed class RestoreOrderByIdRequestValidator
    : FeatureRequestValidator<RestoreOrderByIdRequest, RestoreOrderByIdResponse>
{
    public RestoreOrderByIdRequestValidator()
    {
        RuleFor(request => request.OrderId).NotEmpty();
    }
}
