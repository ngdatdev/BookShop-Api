using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Features;
using BookShop.Application.Shared.Pagination;
using BookShop.Data.Features.UnitOfWork;

namespace BookShop.Application.Features.Product.GetAllTemporarilyRemovedProducts;

/// <summary>
///     GetAllTemporarilyRemovedProducts Handler
/// </summary>
public class GetAllTemporarilyRemovedProductsHandler
    : IFeatureHandler<
        GetAllTemporarilyRemovedProductsRequest,
        GetAllTemporarilyRemovedProductsResponse
    >
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllTemporarilyRemovedProductsHandler(IUnitOfWork unitOfWork)
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
    public async Task<GetAllTemporarilyRemovedProductsResponse> HandlerAsync(
        GetAllTemporarilyRemovedProductsRequest request,
        CancellationToken cancellationToken
    )
    {
        // Find products is temporaliry removed.
        var products =
            await _unitOfWork.ProductFeature.GetAllTemporarilyRemovedProductsRepository.FindTemporarilyRemovedProductsQueryAsync(
                pageIndex: request.PageIndex,
                PageSize: request.PageSize,
                cancellationToken: cancellationToken
            );

        // Response successfully.
        return new GetAllTemporarilyRemovedProductsResponse()
        {
            StatusCode = GetAllTemporarilyRemovedProductsResponseStatusCode.OPERATION_SUCCESS,
            ResponseBody = new()
            {
                Products =
                    new PaginationResponse<GetAllTemporarilyRemovedProductsResponse.Body.Product>()
                    {
                        Contents = products.Select(
                            product => new GetAllTemporarilyRemovedProductsResponse.Body.Product()
                            {
                                FullName = product.FullName,
                                Author = product.Author,
                                ImageUrl = product.ImageUrl,
                                Discount = $"{product.Discount}%",
                                OriginalPrice = product.Price.ToString("0.000"),
                                SalePrice = (
                                    product.Price * (1 - (product.Discount / 100.0m))
                                ).ToString("0.000"),
                            }
                        )
                    }
            }
        };
    }
}
