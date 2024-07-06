using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Features;
using BookShop.Data.Features.UnitOfWork;

namespace BookShop.Application.Features.Addresses.GetAllWardsByDistrictName;

/// <summary>
///     GetAllWardsByDistrictName Handler
/// </summary>
public class GetAllWardsByDistrictNameHandler
    : IFeatureHandler<GetAllWardsByDistrictNameRequest, GetAllWardsByDistrictNameResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllWardsByDistrictNameHandler(IUnitOfWork unitOfWork)
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
    public async Task<GetAllWardsByDistrictNameResponse> HandlerAsync(
        GetAllWardsByDistrictNameRequest request,
        CancellationToken cancellationToken
    )
    {
        // Find all wards by district name.
        var wards =
            await _unitOfWork.AddressFeature.GetAllWardsByDistrictNameRepository.FindAllWardsByDistrictNameQueryAsync(
                districtName: request.District.Trim(),
                cancellationToken: cancellationToken
            );

        // Response successfully.
        return new GetAllWardsByDistrictNameResponse()
        {
            StatusCode = GetAllWardsByDistrictNameResponseStatusCode.OPERATION_SUCCESS,
            ResponseBody = new() { Wards = wards.ToList() }
        };
    }
}
