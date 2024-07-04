using System.IO;
using System.Linq;
using BookShop.Application.Shared.Features;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace BookShop.Application.Features.Roles.CreateRole;

/// <summary>
///    CreateRole Request Validator
/// </summary>
public sealed class CreateRoleRequestValidator
    : FeatureRequestValidator<CreateRoleRequest, CreateRoleResponse>
{
    public CreateRoleRequestValidator()
    {
        RuleFor(expression: request => request.RoleName)
            .NotEmpty()
            .MinimumLength(minimumLength: Data.Shared.Entities.Role.MetaData.Name.MinLength)
            .MaximumLength(maximumLength: Data.Shared.Entities.Role.MetaData.Name.MaxLength);
    }
}
