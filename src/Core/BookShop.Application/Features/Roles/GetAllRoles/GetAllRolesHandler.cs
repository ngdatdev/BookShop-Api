using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Features;
using BookShop.Data.Features.UnitOfWork;

namespace BookShop.Application.Features.Roles.GetAllRoles;

/// <summary>
///     GetAllRoles Handler
/// </summary>
public class GetAllRolesHandler : IFeatureHandler<GetAllRolesRequest, GetAllRolesResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllRolesHandler(IUnitOfWork unitOfWork)
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
    public async Task<GetAllRolesResponse> HandlerAsync(
        GetAllRolesRequest request,
        CancellationToken cancellationToken
    )
    {
        // Find all roles query.
        var roles = await _unitOfWork.RoleFeature.GetAllRolesRepository.FindAllRolesQueryAsync(
            cancellationToken: cancellationToken
        );

        // Response successfully.
        return new GetAllRolesResponse()
        {
            StatusCode = GetAllRolesResponseStatusCode.OPERATION_SUCCESS,

            ResponseBody = new()
            {
                Roles = roles.Select(role => new GetAllRolesResponse.Body.Role
                {
                    RoleId = role.Id,
                    RoleName = role.Name,
                })
            }
        };
    }
}
