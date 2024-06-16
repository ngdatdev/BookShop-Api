using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Features;
using BookShop.Data.Features.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;

namespace BookShop.Application.Features.Address.GetAllAddresses;

/// <summary>
///     GetAllAddresses Handler
/// </summary>
public class GetAllAddressesHandler
    : IFeatureHandler<GetAllAddressesRequest, GetAllAddressesResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _contextAccessor;

    public GetAllAddressesHandler(IUnitOfWork unitOfWork, IHttpContextAccessor contextAccessor)
    {
        _unitOfWork = unitOfWork;
        _contextAccessor = contextAccessor;
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
    public async Task<GetAllAddressesResponse> HandlerAsync(
        GetAllAddressesRequest request,
        CancellationToken cancellationToken
    )
    {
        // Response successfully.
        return new GetAllAddressesResponse()
        {
            StatusCode = GetAllAddressesResponseStatusCode.OPERATION_SUCCESS,
            ResponseBody = new() { }
        };
    }
}
