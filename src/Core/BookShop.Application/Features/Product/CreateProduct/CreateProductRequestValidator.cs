using System.IO;
using System.Linq;
using BookShop.Application.Shared.Features;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace BookShop.Application.Features.Product.CreateProduct;

/// <summary>
///    CreateProduct Request Validator
/// </summary>
public sealed class CreateProductRequestValidator
    : FeatureRequestValidator<CreateProductRequest, CreateProductResponse>
{
    public CreateProductRequestValidator()
    {
        RuleFor(expression: request => request.FullName)
            .NotEmpty()
            .MinimumLength(
                minimumLength: BookShop.Data.Shared.Entities.Product.MetaData.FullName.MinLength
            )
            .MaximumLength(
                maximumLength: BookShop.Data.Shared.Entities.Product.MetaData.FullName.MaxLength
            );

        RuleFor(expression: request => request.Description)
            .NotEmpty()
            .MinimumLength(
                minimumLength: BookShop.Data.Shared.Entities.Product.MetaData.Description.MinLength
            );

        RuleFor(expression: request => request.Price).NotEmpty().GreaterThan(0);

        RuleFor(expression: request => request.Discount).NotEmpty().GreaterThan(0).LessThan(100);

        RuleFor(expression: request => request.Size)
            .NotEmpty()
            .Matches(@"^\d+(\.\d+)? x \d+(\.\d+)? x \d+(\.\d+)? cm$")
            .WithMessage("Size must be in the format '00 x 00 x 0.0 cm'.");

        RuleFor(expression: request => request.NumberOfPage).NotEmpty().GreaterThan(0);

        RuleFor(expression: request => request.QuantityCurrent).NotEmpty().GreaterThan(0);

        RuleFor(product => product.ImageUrl)
            .NotNull()
            .WithMessage("Image is required.")
            .Must(BeAValidImage)
            .WithMessage("Only JPEG, PNG, and GIF images are allowed.")
            .Must(BeAValidSize)
            .WithMessage("Image must be less than 2MB.");

        RuleFor(expression: request => request.Author)
            .NotEmpty()
            .MinimumLength(
                minimumLength: BookShop.Data.Shared.Entities.Product.MetaData.Author.MinLength
            );

        RuleFor(expression: request => request.Publisher)
            .NotEmpty()
            .MinimumLength(
                minimumLength: BookShop.Data.Shared.Entities.Product.MetaData.Publisher.MinLength
            );

        RuleFor(expression: request => request.Languages)
            .NotEmpty()
            .MinimumLength(
                minimumLength: BookShop.Data.Shared.Entities.Product.MetaData.Languages.MinLength
            )
            .MaximumLength(
                maximumLength: BookShop.Data.Shared.Entities.Product.MetaData.Languages.MaxLength
            );

        RuleForEach(product => product.SubImages)
            .Must(BeAValidImage)
            .WithMessage("Only JPEG, PNG, and GIF images are allowed.")
            .Must(BeAValidSize)
            .WithMessage("Image must be less than 2MB.");
    }

    private bool BeAValidImage(IFormFile file)
    {
        if (Equals(objA: file, objB: default))
            return false;

        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
        return !string.IsNullOrEmpty(extension) && allowedExtensions.Contains(extension);
    }

    private bool BeAValidSize(IFormFile file)
    {
        if (Equals(objA: file, objB: default))
            return false;

        return file.Length < 2 * 1024 * 1024;
    }
}
