using BookShop.Application.Features.Product.GetProductsByCategoryId;
using BookShop.Application.Shared.Features;
using FluentValidation;

namespace BookShop.Application.Features.Auth.GetProductsByCategoryId;

/// <summary>
///     GetProductsByCategoryId Request Validator
/// </summary>
public sealed class GetProductsByCategoryIdRequestValidator
    : FeatureRequestValidator<GetProductsByCategoryIdRequest, GetProductsByCategoryIdResponse>
{
    public GetProductsByCategoryIdRequestValidator()
    {
        RuleFor(x => x.CategoryId).NotEmpty().WithMessage("CategoryId is required.");

        RuleFor(x => x.PageIndex).GreaterThan(0).WithMessage("PageIndex must be greater than 0.");

        RuleFor(x => x.PageSize).GreaterThan(0).WithMessage("PageSize must be greater than 0.");

        RuleFor(x => x.SortField).NotEmpty().WithMessage("SortField is required.");

        RuleFor(x => x.Order)
            .Must(order => order == "asc" || order == "desc")
            .WithMessage("Order must be either 'asc' or 'desc'.");

        RuleFor(x => x.MinPrice)
            .GreaterThanOrEqualTo(0)
            .When(x => x.MinPrice.HasValue)
            .WithMessage("MinPrice must be greater than or equal to 0.");

        RuleFor(x => x.MaxPrice)
            .GreaterThanOrEqualTo(0)
            .When(x => x.MaxPrice.HasValue)
            .WithMessage("MaxPrice must be greater than or equal to 0.");
    }
}
