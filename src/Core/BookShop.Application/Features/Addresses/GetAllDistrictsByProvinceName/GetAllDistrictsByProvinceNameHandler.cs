using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Features;
using BookShop.Data.Features.UnitOfWork;

namespace BookShop.Application.Features.Addresses.GetAllDistrictsByProvinceName;

/// <summary>
///     GetAllDistrictsByProvinceName Handler
/// </summary>
public class GetAllDistrictsByProvinceNameHandler
    : IFeatureHandler<GetAllDistrictsByProvinceNameRequest, GetAllDistrictsByProvinceNameResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllDistrictsByProvinceNameHandler(IUnitOfWork unitOfWork)
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
    public async Task<GetAllDistrictsByProvinceNameResponse> HandlerAsync(
        GetAllDistrictsByProvinceNameRequest request,
        CancellationToken cancellationToken
    )
    {
        // Find all districts by province name.
        var districts =
            await _unitOfWork.AddressFeature.GetAllDistrictsByProvinceNameRepository.FindAllDistrictsByProvinceNameQueryAsync(
                provinceName: request.Province.Trim(),
                cancellationToken: cancellationToken
            );

        // Response successfully.
        return new GetAllDistrictsByProvinceNameResponse()
        {
            StatusCode = GetAllDistrictsByProvinceNameResponseStatusCode.OPERATION_SUCCESS,
            ResponseBody = new() { Districts = districts.ToList() }
        };
    }
}
