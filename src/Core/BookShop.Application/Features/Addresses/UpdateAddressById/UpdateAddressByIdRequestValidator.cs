using System.IO;
using System.Linq;
using BookShop.Application.Shared.Features;
using BookShop.Data.Shared.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace BookShop.Application.Features.Addresses.UpdateAddressById;

/// <summary>
///    UpdateAddressById Request Validator
/// </summary>
public sealed class UpdateAddressByIdRequestValidator
    : FeatureRequestValidator<UpdateAddressByIdRequest, UpdateAddressByIdResponse>
{
    public UpdateAddressByIdRequestValidator()
    {
        RuleFor(request => request.AddressId).NotEmpty();

        RuleFor(request => request.Ward)
            .NotEmpty()
            .MaximumLength(maximumLength: Address.MetaData.Ward.MaxLength);

        RuleFor(request => request.Ward)
            .NotEmpty()
            .MinimumLength(minimumLength: Address.MetaData.Ward.MinLength);
    }
}
