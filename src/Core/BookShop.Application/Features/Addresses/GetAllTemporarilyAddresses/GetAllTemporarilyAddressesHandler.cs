using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Features;
using BookShop.Data.Features.UnitOfWork;

namespace BookShop.Application.Features.Addresses.GetAllTemporarilyAddresses;

/// <summary>
///     GetAllTemporarilyAddresses Handler
/// </summary>
public class GetAllTemporarilyAddressesHandler
    : IFeatureHandler<GetAllTemporarilyAddressesRequest, GetAllTemporarilyAddressesResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllTemporarilyAddressesHandler(IUnitOfWork unitOfWork)
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
    public async Task<GetAllTemporarilyAddressesResponse> HandlerAsync(
        GetAllTemporarilyAddressesRequest request,
        CancellationToken cancellationToken
    )
    {
        // Find all address by ward name.
        var addresses =
            await _unitOfWork.AddressFeature.GetAllTemporarilyAddressesRepository.FindAllTemporarilyAddressesQueryAsync(
                cancellationToken: cancellationToken
            );

        // Response successfully.
        return new GetAllTemporarilyAddressesResponse()
        {
            StatusCode = GetAllTemporarilyAddressesResponseStatusCode.OPERATION_SUCCESS,
            ResponseBody = new()
            {
                Addresses = addresses.Select(
                    address => new GetAllTemporarilyAddressesResponse.Body.Address()
                    {
                        AddressId = address.Id,
                        Ward = address.Ward,
                        District = address.District,
                        Province = address.Province,
                    }
                )
            }
        };
    }
}
