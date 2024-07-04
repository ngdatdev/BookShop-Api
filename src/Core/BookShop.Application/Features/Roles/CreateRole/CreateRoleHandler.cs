using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Common;
using BookShop.Application.Shared.Features;
using BookShop.Data.Features.UnitOfWork;
using BookShop.Data.Shared.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;

namespace BookShop.Application.Features.Roles.CreateRole;

/// <summary>
///     CreateRole Handler
/// </summary>
public class CreateRoleHandler : IFeatureHandler<CreateRoleRequest, CreateRoleResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private IHttpContextAccessor _httpContextAccessor;

    public CreateRoleHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _httpContextAccessor = httpContextAccessor;
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
    public async Task<CreateRoleResponse> HandlerAsync(
        CreateRoleRequest request,
        CancellationToken cancellationToken
    )
    {
        // Is role with the same role name found.
        var isSameName =
            await _unitOfWork.RoleFeature.CreateRoleRepository.IsSameRoleNameFoundByRoleNameQueryAsync(
                newRoleName: request.RoleName,
                cancellationToken: cancellationToken
            );

        // Continue handle if role name is the same.
        if (isSameName)
        {
            var isRoleTemporarilyRemoved =
                await _unitOfWork.RoleFeature.CreateRoleRepository.IsRoleTemporarilyRemovedByRoleNameQueryAsync(
                    newRoleName: request.RoleName,
                    cancellationToken: cancellationToken
                );

            if (isRoleTemporarilyRemoved)
            {
                return new()
                {
                    StatusCode = CreateRoleResponseStatusCode.ROLE_IS_ALREADY_TEMPORARILY_REMOVED,
                };
            }

            return new() { StatusCode = CreateRoleResponseStatusCode.ROLE_ALREADY_EXISTS, };
        }

        // Get userId from claim.
        var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(
            claimType: JwtRegisteredClaimNames.Sub
        );

        // Init new role
        var newRole = InitRole(createRoleRequest: request, userId: Guid.Parse(userId));

        // Create role command.
        var dbResult = await _unitOfWork.RoleFeature.CreateRoleRepository.CreateRoleCommandAsync(
            newRole: newRole,
            cancellationToken: cancellationToken
        );

        // Responds if database is fail.
        if (!dbResult)
        {
            return new() { StatusCode = CreateRoleResponseStatusCode.DATABASE_OPERATION_FAIL, };
        }

        // Response successfully.
        return new CreateRoleResponse()
        {
            StatusCode = CreateRoleResponseStatusCode.OPERATION_SUCCESS,
        };
    }

    private Role InitRole(CreateRoleRequest createRoleRequest, Guid userId)
    {
        return new()
        {
            Id = Guid.NewGuid(),
            Name = createRoleRequest.RoleName,
            RoleDetail = new()
            {
                CreatedAt = DateTime.UtcNow,
                CreatedBy = userId,
                RemovedAt = CommonConstant.MIN_DATE_TIME,
                RemovedBy = CommonConstant.DEFAULT_ENTITY_ID_AS_GUID,
                UpdatedAt = CommonConstant.MIN_DATE_TIME,
                UpdatedBy = CommonConstant.DEFAULT_ENTITY_ID_AS_GUID,
            }
        };
    }
}
