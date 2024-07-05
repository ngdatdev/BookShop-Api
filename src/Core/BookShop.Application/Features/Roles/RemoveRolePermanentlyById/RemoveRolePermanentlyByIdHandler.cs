using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Features;
using BookShop.Data.Features.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;

namespace BookShop.Application.Features.Roles.RemoveRolePermanentlyById;

/// <summary>
///     RemoveRolePermanentlyById Handler
/// </summary>
public class RemoveRolePermanentlyByIdHandler
    : IFeatureHandler<RemoveRolePermanentlyByIdRequest, RemoveRolePermanentlyByIdResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public RemoveRolePermanentlyByIdHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
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
    public async Task<RemoveRolePermanentlyByIdResponse> HandlerAsync(
        RemoveRolePermanentlyByIdRequest request,
        CancellationToken cancellationToken
    )
    {
        // Is role found by role id.
        var isRoleFound =
            await _unitOfWork.RoleFeature.RemoveRolePermanentlyByIdRepository.IsRoleFoundByIdQueryAsync(
                roleId: request.RoleId,
                cancellationToken: cancellationToken
            );

        // Reponds if role is not found.
        if (!isRoleFound)
        {
            return new()
            {
                StatusCode = RemoveRolePermanentlyByIdResponseStatusCode.ROLE_IS_NOT_FOUND,
            };
        }

        // Is role temporarily removed.
        var isRoleTemporarilyRemoved =
            await _unitOfWork.RoleFeature.RemoveRolePermanentlyByIdRepository.IsRoleTemporarilyRemovedByIdQueryAsync(
                roleId: request.RoleId,
                cancellationToken: cancellationToken
            );

        // Responds if role is already temporarily removed.
        if (!isRoleTemporarilyRemoved)
        {
            return new()
            {
                StatusCode =
                    RemoveRolePermanentlyByIdResponseStatusCode.ROLE_IS_NOT_TEMPORARILY_REMOVED
            };
        }

        // Remove role by id command.
        var dbResult =
            await _unitOfWork.RoleFeature.RemoveRolePermanentlyByIdRepository.DeleteRolePermanentlyByIdCommandAsync(
                roleId: request.RoleId,
                cancellationToken: cancellationToken
            );

        // Responds if database is fail.
        if (!dbResult)
        {
            return new()
            {
                StatusCode = RemoveRolePermanentlyByIdResponseStatusCode.DATABASE_OPERATION_FAIL,
            };
        }

        // Response successfully.
        return new RemoveRolePermanentlyByIdResponse()
        {
            StatusCode = RemoveRolePermanentlyByIdResponseStatusCode.OPERATION_SUCCESS,
        };
    }
}
