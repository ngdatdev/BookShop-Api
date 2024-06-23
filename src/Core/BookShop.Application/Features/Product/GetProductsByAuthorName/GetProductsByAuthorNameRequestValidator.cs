using System;
using System.IO;
using System.Linq;
using BookShop.Application.Shared.Features;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace BookShop.Application.Features.Product.GetProductsByAuthorName;

/// <summary>
///    GetProductsByAuthorName Request Validator
/// </summary>
public sealed class GetProductsByAuthorNameRequestValidator
    : FeatureRequestValidator<GetProductsByAuthorNameRequest, GetProductsByAuthorNameResponse>
{
    public GetProductsByAuthorNameRequestValidator()
    {
        RuleFor(request => request.AuthorName).NotEmpty();

        RuleFor(request => request.PageIndex)
            .GreaterThanOrEqualTo(1)
            .WithMessage("PageIndex must be at least 1.");

        RuleFor(request => request.PageSize)
            .GreaterThan(0)
            .WithMessage("PageSize must be greater than 0.")
            .LessThanOrEqualTo(100)
            .WithMessage("PageSize must be less than or equal to 100.");

        RuleFor(request => request.SortField)
            .NotEmpty()
            .WithMessage("SortField is required.")
            .Must(sortField => new[] { "Newest", "Price", "MostBuy", "Name" }.Contains(sortField))
            .WithMessage("SortField must be one of the following: Newest, Price, MostBuy, Name.");

        RuleFor(request => request.Order)
            .NotEmpty()
            .WithMessage("Order is required.")
            .Must(order =>
                order.Equals("asc", StringComparison.OrdinalIgnoreCase)
                || order.Equals("desc", StringComparison.OrdinalIgnoreCase)
            )
            .WithMessage("Order must be either 'asc' or 'desc'.");

        RuleFor(request => request.MinPrice)
            .GreaterThanOrEqualTo(0)
            .WithMessage("MinPrice must be greater than or equal to 0.")
            .LessThanOrEqualTo(request => request.MaxPrice ?? decimal.MaxValue)
            .WithMessage("MinPrice must be less than or equal to MaxPrice.");

        RuleFor(request => request.MaxPrice)
            .GreaterThanOrEqualTo(0)
            .WithMessage("MaxPrice must be greater than or equal to 0.")
            .GreaterThanOrEqualTo(request => request.MinPrice ?? decimal.MinValue)
            .WithMessage("MaxPrice must be greater than or equal to MinPrice.");
    }
}
