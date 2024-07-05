using System.IO;
using System.Linq;
using BookShop.Application.Shared.Features;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace BookShop.Application.Features.Roles.UpdateRoleById;

/// <summary>
///    UpdateRoleById Request Validator
/// </summary>
public sealed class UpdateRoleByIdRequestValidator
    : FeatureRequestValidator<UpdateRoleByIdRequest, UpdateRoleByIdResponse>
{
    public UpdateRoleByIdRequestValidator()
    {
        RuleFor(expression: request => request.RoleId).NotEmpty();

        RuleFor(expression: request => request.RoleName)
            .NotEmpty()
            .MinimumLength(minimumLength: Data.Shared.Entities.Role.MetaData.Name.MinLength)
            .MaximumLength(maximumLength: Data.Shared.Entities.Role.MetaData.Name.MaxLength);
    }
}
