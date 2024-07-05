using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Features;
using BookShop.Data.Features.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;

namespace BookShop.Application.Features.Roles.RestoreRoleById;

/// <summary>
///     RestoreRoleById Handler
/// </summary>
public class RestoreRoleByIdHandler
    : IFeatureHandler<RestoreRoleByIdRequest, RestoreRoleByIdResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public RestoreRoleByIdHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
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
    public async Task<RestoreRoleByIdResponse> HandlerAsync(
        RestoreRoleByIdRequest request,
        CancellationToken cancellationToken
    )
    {
        // Is role found by role id.
        var isRoleFound =
            await _unitOfWork.RoleFeature.RestoreRoleByIdRepository.IsRoleFoundByIdQueryAsync(
                roleId: request.RoleId,
                cancellationToken: cancellationToken
            );

        // Reponds if role is not found.
        if (!isRoleFound)
        {
            return new() { StatusCode = RestoreRoleByIdResponseStatusCode.ROLE_IS_NOT_FOUND, };
        }

        // Is role temporarily removed.
        var isRoleTemporarilyRemoved =
            await _unitOfWork.RoleFeature.RestoreRoleByIdRepository.IsRoleTemporarilyRemovedByIdQueryAsync(
                roleId: request.RoleId,
                cancellationToken: cancellationToken
            );

        // Responds if role is already temporarily removed.
        if (!isRoleTemporarilyRemoved)
        {
            return new()
            {
                StatusCode = RestoreRoleByIdResponseStatusCode.ROLE_IS_NOT_TEMPORARILY_REMOVED
            };
        }

        // Remove role by id command.
        var dbResult =
            await _unitOfWork.RoleFeature.RestoreRoleByIdRepository.RestoreRoleTemporarilyByIdCommandAsync(
                roleId: request.RoleId,
                cancellationToken: cancellationToken
            );

        // Responds if database is fail.
        if (!dbResult)
        {
            return new()
            {
                StatusCode = RestoreRoleByIdResponseStatusCode.DATABASE_OPERATION_FAIL,
            };
        }

        // Response successfully.
        return new RestoreRoleByIdResponse()
        {
            StatusCode = RestoreRoleByIdResponseStatusCode.OPERATION_SUCCESS,
        };
    }
}
