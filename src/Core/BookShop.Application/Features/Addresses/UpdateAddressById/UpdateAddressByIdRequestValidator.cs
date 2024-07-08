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
            .MaximumLength(maximumLength: Address.MetaData.Ward.MaxLength)
            .MinimumLength(minimumLength: Address.MetaData.Ward.MinLength);

        RuleFor(request => request.District)
            .NotEmpty()
            .MinimumLength(minimumLength: Address.MetaData.District.MinLength)
            .MaximumLength(maximumLength: Address.MetaData.District.MaxLength);

        RuleFor(request => request.Province)
            .NotEmpty()
            .MinimumLength(minimumLength: Address.MetaData.Province.MinLength)
            .MaximumLength(maximumLength: Address.MetaData.Province.MinLength);
    }
}
