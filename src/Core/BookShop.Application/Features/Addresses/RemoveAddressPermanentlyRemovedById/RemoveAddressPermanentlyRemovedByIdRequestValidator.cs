using BookShop.Application.Shared.Features;
using FluentValidation;

namespace BookShop.Application.Features.Addresses.RemoveAddressPermanentlyRemovedById;

/// <summary>
///    RemoveAddressPermanentlyRemovedById Request Validator
/// </summary>
public sealed class RemoveAddressPermanentlyRemovedByIdRequestValidator
    : FeatureRequestValidator<
        RemoveAddressPermanentlyRemovedByIdRequest,
        RemoveAddressPermanentlyRemovedByIdResponse
    >
{
    public RemoveAddressPermanentlyRemovedByIdRequestValidator()
    {
        RuleFor(request => request.AddressId).NotEmpty();
    }
}
