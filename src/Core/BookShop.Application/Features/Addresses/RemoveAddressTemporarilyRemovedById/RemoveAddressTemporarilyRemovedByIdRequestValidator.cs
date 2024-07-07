using BookShop.Application.Shared.Features;
using FluentValidation;

namespace BookShop.Application.Features.Addresses.RemoveAddressTemporarilyRemovedById;

/// <summary>
///    RemoveAddressTemporarilyRemovedById Request Validator
/// </summary>
public sealed class RemoveAddressTemporarilyRemovedByIdRequestValidator
    : FeatureRequestValidator<
        RemoveAddressTemporarilyRemovedByIdRequest,
        RemoveAddressTemporarilyRemovedByIdResponse
    >
{
    public RemoveAddressTemporarilyRemovedByIdRequestValidator()
    {
        RuleFor(request => request.AddressId).NotEmpty();
    }
}
