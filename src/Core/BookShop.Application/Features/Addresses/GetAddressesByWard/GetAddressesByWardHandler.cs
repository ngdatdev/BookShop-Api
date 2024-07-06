using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Features;
using BookShop.Data.Features.UnitOfWork;

namespace BookShop.Application.Features.Addresses.GetAddressesByWard;

/// <summary>
///     GetAddressesByWard Handler
/// </summary>
public class GetAddressesByWardHandler
    : IFeatureHandler<GetAddressesByWardRequest, GetAddressesByWardResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAddressesByWardHandler(IUnitOfWork unitOfWork)
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
    public async Task<GetAddressesByWardResponse> HandlerAsync(
        GetAddressesByWardRequest request,
        CancellationToken cancellationToken
    )
    {
        // Find all address by ward name.
        var addresses =
            await _unitOfWork.AddressFeature.GetAddressesByWardRepository.FindAllAddressesByWardNameQueryAsync(
                ward: request.Ward.Trim(),
                cancellationToken: cancellationToken
            );

        // Response successfully.
        return new GetAddressesByWardResponse()
        {
            StatusCode = GetAddressesByWardResponseStatusCode.OPERATION_SUCCESS,
            ResponseBody = new()
            {
                Addresses = addresses.Select(
                    address => new GetAddressesByWardResponse.Body.Address()
                    {
                        Ward = address.Ward,
                        District = address.District,
                        Province = address.Province,
                    }
                )
            }
        };
    }
}
