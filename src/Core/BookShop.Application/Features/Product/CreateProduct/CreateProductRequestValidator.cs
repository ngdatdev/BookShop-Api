using BookShop.Application.Shared.Features;
using FluentValidation;

namespace BookShop.Application.Features.Product.CreateProduct;

/// <summary>
///     Hello world Request Validator
/// </summary>
public sealed class CreateProductRequestValidator
    : FeatureRequestValidator<CreateProductRequest, CreateProductResponse>
{
    public CreateProductRequestValidator()
    {
        //ClassLevelCascadeMode = CascadeMode.Stop;

        //RuleFor(expression: request => request.Name)
        //    .Cascade(cascadeMode: CascadeMode.Stop)
        //    .NotEmpty()
        //    .MinimumLength(minimumLength: User.MetaData.UserName.MinLength);
    }
}
