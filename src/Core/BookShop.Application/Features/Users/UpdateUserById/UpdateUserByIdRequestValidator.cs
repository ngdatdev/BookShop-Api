using System;
using System.IO;
using System.Linq;
using BookShop.Application.Shared.Features;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;

namespace BookShop.Application.Features.Users.UpdateUserById;

/// <summary>
///    UpdateUserById Request Validator
/// </summary>
public sealed class UpdateUserByIdRequestValidator
    : FeatureRequestValidator<UpdateUserByIdRequest, UpdateUserByIdResponse>
{
    public UpdateUserByIdRequestValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();

        RuleFor(x => x.Username)
            .NotEmpty()
            .Length(
                min: Data.Shared.Entities.User.MetaData.UserName.MinLength,
                max: Data.Shared.Entities.User.MetaData.UserName.MaxLength
            );

        RuleFor(x => x.NumberPhone).NotEmpty().Matches(@"^\+?\d{10,15}$");

        RuleFor(x => x.OldAvatarUrl)
            .Must(uri => Uri.IsWellFormedUriString(uri, UriKind.RelativeOrAbsolute));

        RuleFor(product => product.NewAvatarUrl)
            .Must(BeAValidImage)
            .WithMessage("Only JPEG, PNG, and GIF images are allowed.")
            .Must(BeAValidSize)
            .WithMessage("Image must be less than 2MB.");

        RuleFor(x => x.FirstName)
            .NotEmpty()
            .MaximumLength(
                maximumLength: Data.Shared.Entities.UserDetail.MetaData.FirstName.MaxLength
            )
            .MinimumLength(
                minimumLength: Data.Shared.Entities.UserDetail.MetaData.FirstName.MinLength
            );

        RuleFor(x => x.LastName)
            .NotEmpty()
            .MaximumLength(
                maximumLength: Data.Shared.Entities.UserDetail.MetaData.LastName.MaxLength
            )
            .MinimumLength(
                minimumLength: Data.Shared.Entities.UserDetail.MetaData.LastName.MinLength
            );

        RuleFor(x => x.Gender).NotEmpty();

        RuleFor(x => x.DateOfBirth).NotEmpty().LessThan(DateTime.Now);

        RuleFor(x => x.Address).NotEmpty().Matches(@"^.+<token\/>.+<token\/>.+$");
    }

    private bool BeAValidImage(IFormFile file)
    {
        if (Equals(objA: file, objB: default))
            return true;
        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
        return !string.IsNullOrEmpty(extension) && allowedExtensions.Contains(extension);
    }

    private bool BeAValidSize(IFormFile file)
    {
        if (Equals(objA: file, objB: default))
            return true;

        return file.Length < 2 * 1024 * 1024;
    }
}
