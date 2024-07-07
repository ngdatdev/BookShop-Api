using BookShop.Application.Shared.Features;
using FluentValidation;

namespace BookShop.Application.Features.Addresses.RestoreAddressById;

/// <summary>
///    RestoreAddressById Request Validator
/// </summary>
public sealed class RestoreAddressByIdRequestValidator
    : FeatureRequestValidator<RestoreAddressByIdRequest, RestoreAddressByIdResponse>
{
    public RestoreAddressByIdRequestValidator()
    {
        RuleFor(request => request.AddressId).NotEmpty();
    }
}
