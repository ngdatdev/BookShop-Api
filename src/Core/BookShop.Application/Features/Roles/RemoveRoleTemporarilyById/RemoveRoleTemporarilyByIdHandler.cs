using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Features;
using BookShop.Data.Features.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;

namespace BookShop.Application.Features.Roles.RemoveRoleTemporarilyById;

/// <summary>
///     RemoveRoleTemporarilyById Handler
/// </summary>
public class RemoveRoleTemporarilyByIdHandler
    : IFeatureHandler<RemoveRoleTemporarilyByIdRequest, RemoveRoleTemporarilyByIdResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public RemoveRoleTemporarilyByIdHandler(
        IUnitOfWork unitOfWork,
        IHttpContextAccessor httpContextAccessor
    )
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
    public async Task<RemoveRoleTemporarilyByIdResponse> HandlerAsync(
        RemoveRoleTemporarilyByIdRequest request,
        CancellationToken cancellationToken
    )
    {
        // Is role found by role id.
        var isRoleFound =
            await _unitOfWork.RoleFeature.RemoveRoleTemporarilyByIdRepository.IsRoleFoundByIdQueryAsync(
                roleId: request.RoleId,
                cancellationToken: cancellationToken
            );

        // Reponds if role is not found.
        if (!isRoleFound)
        {
            return new()
            {
                StatusCode = RemoveRoleTemporarilyByIdResponseStatusCode.ROLE_IS_NOT_FOUND,
            };
        }

        // Is role temporarily removed.
        var isRoleTemporarilyRemoved =
            await _unitOfWork.RoleFeature.RemoveRoleTemporarilyByIdRepository.IsRoleTemporarilyRemovedByIdQueryAsync(
                roleId: request.RoleId,
                cancellationToken: cancellationToken
            );

        // Responds if role is already temporarily removed.
        if (isRoleTemporarilyRemoved)
        {
            return new()
            {
                StatusCode =
                    RemoveRoleTemporarilyByIdResponseStatusCode.ROLE_IS_ALREADY_TEMPORARILY_REMOVED
            };
        }

        // Get userId from claim.
        var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(
            claimType: JwtRegisteredClaimNames.Sub
        );

        // Remove role by id command.
        var dbResult =
            await _unitOfWork.RoleFeature.RemoveRoleTemporarilyByIdRepository.DeleteRoleTemporarilyByIdCommandAsync(
                roleId: request.RoleId,
                removedAt: DateTime.UtcNow,
                removedBy: Guid.Parse(userId),
                cancellationToken: cancellationToken
            );

        // Responds if database is fail.
        if (!dbResult)
        {
            return new()
            {
                StatusCode = RemoveRoleTemporarilyByIdResponseStatusCode.DATABASE_OPERATION_FAIL,
            };
        }

        // Response successfully.
        return new RemoveRoleTemporarilyByIdResponse()
        {
            StatusCode = RemoveRoleTemporarilyByIdResponseStatusCode.OPERATION_SUCCESS,
        };
    }
}
