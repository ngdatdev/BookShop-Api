using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Features;
using BookShop.Data.Features.UnitOfWork;

namespace BookShop.Application.Features.Roles.GetAllRolesTemporarilyRemoved;

/// <summary>
///     GetAllRolesTemporarilyRemoved Handler
/// </summary>
public class GetAllRolesTemporarilyRemovedHandler
    : IFeatureHandler<GetAllRolesTemporarilyRemovedRequest, GetAllRolesTemporarilyRemovedResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllRolesTemporarilyRemovedHandler(IUnitOfWork unitOfWork)
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
    public async Task<GetAllRolesTemporarilyRemovedResponse> HandlerAsync(
        GetAllRolesTemporarilyRemovedRequest request,
        CancellationToken cancellationToken
    )
    {
        // Find all roles query.
        var roles =
            await _unitOfWork.RoleFeature.GetAllRolesTemporarilyRemovedRepository.FindAllRolesTemporarilyRemovedQueryAsync(
                cancellationToken: cancellationToken
            );

        // Response successfully.
        return new GetAllRolesTemporarilyRemovedResponse()
        {
            StatusCode = GetAllRolesTemporarilyRemovedResponseStatusCode.OPERATION_SUCCESS,

            ResponseBody = new()
            {
                Roles = roles.Select(role => new GetAllRolesTemporarilyRemovedResponse.Body.Role
                {
                    RoleId = role.Id,
                    RoleName = role.Name,
                })
            }
        };
    }
}
