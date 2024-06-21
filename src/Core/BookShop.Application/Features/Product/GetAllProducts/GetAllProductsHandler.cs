using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Authentication.Jwt;
using BookShop.Application.Shared.Features;
using BookShop.Application.Shared.Pagination;
using BookShop.Data.Features.UnitOfWork;
using BookShop.Data.Shared.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.JsonWebTokens;

namespace BookShop.Application.Features.Product.GetAllProducts;

/// <summary>
///     GetAllProducts Handler
/// </summary>
public class GetAllProductsHandler : IFeatureHandler<GetAllProductsRequest, GetAllProductsResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllProductsHandler(IUnitOfWork unitOfWork)
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
    public async Task<GetAllProductsResponse> HandlerAsync(
        GetAllProductsRequest request,
        CancellationToken cancellationToken
    )
    {
        // Find products is not removed temporaliry.
        var products =
            await _unitOfWork.ProductFeature.GetAllProductsRepository.GetAllProductsQueryAsync(
                pageIndex: request.PageIndex,
                PageSize: request.PageSize,
                cancellationToken: cancellationToken
            );

        // Response successfully.
        return new GetAllProductsResponse()
        {
            StatusCode = GetAllProductsResponseStatusCode.OPERATION_SUCCESS,
            ResponseBody = new()
            {
                Products = new PaginationResponse<GetAllProductsResponse.Body.Product>()
                {
                    Contents = products.Select(product => new GetAllProductsResponse.Body.Product()
                    {
                        FullName = product.FullName,
                        Author = product.Author,
                        ImageUrl = product.ImageUrl,
                        Discount = $"{product.Discount}%",
                        OriginalPrice = product.Price.ToString("0.000"),
                        SalePrice = (product.Price * (1 - (product.Discount / 100.0m))).ToString(
                            "0.000"
                        ),
                    })
                }
            }
        };
    }
}
