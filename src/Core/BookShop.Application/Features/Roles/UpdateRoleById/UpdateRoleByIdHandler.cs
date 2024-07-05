using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Features;
using BookShop.Data.Features.UnitOfWork;
using BookShop.Data.Shared.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.JsonWebTokens;

namespace BookShop.Application.Features.Roles.UpdateRoleById;

/// <summary>
///     UpdateRoleById Handler
/// </summary>
public class UpdateRoleByIdHandler : IFeatureHandler<UpdateRoleByIdRequest, UpdateRoleByIdResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private IHttpContextAccessor _httpContextAccessor;
    private RoleManager<Role> _roleManager;

    public UpdateRoleByIdHandler(
        IUnitOfWork unitOfWork,
        IHttpContextAccessor httpContextAccessor,
        RoleManager<Role> roleManager
    )
    {
        _unitOfWork = unitOfWork;
        _httpContextAccessor = httpContextAccessor;
        _roleManager = roleManager;
    }

    /// <summary>
    ///     Entry of new request handler.
    /// </summary>
    /// <param name="request">
    ///     Request model.
    /// </param>
    /// <param name="ct">
    ///     A token that is used for notifying system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     A task containing the response.
    public async Task<UpdateRoleByIdResponse> HandlerAsync(
        UpdateRoleByIdRequest request,
        CancellationToken cancellationToken
    )
    {
        // Is role with the same role name found.
        var isSameName =
            await _unitOfWork.RoleFeature.UpdateRoleByIdRepository.IsSameRoleNameFoundByRoleNameQueryAsync(
                roleName: request.RoleName,
                cancellationToken: cancellationToken
            );

        // Continue handle if role name is the same.
        if (isSameName)
        {
            return new()
            {
                StatusCode = UpdateRoleByIdResponseStatusCode.ROLE_NAME_IS_ALREADY_EXISTS,
            };
        }

        var isRoleFoundById =
            await _unitOfWork.RoleFeature.UpdateRoleByIdRepository.IsRoleFoundByIdQueryAsync(
                roleId: request.RoleId,
                cancellationToken: cancellationToken
            );

        if (!isRoleFoundById)
        {
            return new() { StatusCode = UpdateRoleByIdResponseStatusCode.ROLE_IS_NOT_FOUND, };
        }

        var isRoleTemporarilyRemoved =
            await _unitOfWork.RoleFeature.UpdateRoleByIdRepository.IsRoleTemporarilyRemovedByIdQueryAsync(
                roleId: request.RoleId,
                cancellationToken: cancellationToken
            );

        if (isRoleTemporarilyRemoved)
        {
            return new()
            {
                StatusCode = UpdateRoleByIdResponseStatusCode.ROLE_IS_TEMPORARILY_REMOVED,
            };
        }

        // Get userId from claim.
        var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(
            claimType: JwtRegisteredClaimNames.Sub
        );

        // Create role command.
        var dbResult =
            await _unitOfWork.RoleFeature.UpdateRoleByIdRepository.UpdateRoleByIdCommandAsync(
                roleId: request.RoleId,
                roleName: request.RoleName,
                updatedAt: DateTime.UtcNow,
                updatedBy: Guid.Parse(input: userId),
                cancellationToken: cancellationToken
            );

        // Responds if database is fail.
        if (!dbResult)
        {
            return new() { StatusCode = UpdateRoleByIdResponseStatusCode.DATABASE_OPERATION_FAIL, };
        }

        // Response successfully.
        return new UpdateRoleByIdResponse()
        {
            StatusCode = UpdateRoleByIdResponseStatusCode.OPERATION_SUCCESS,
        };
    }
}
