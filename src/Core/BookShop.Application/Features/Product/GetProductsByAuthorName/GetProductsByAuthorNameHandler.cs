using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Features;
using BookShop.Application.Shared.Pagination;
using BookShop.Data.Features.UnitOfWork;

namespace BookShop.Application.Features.Product.GetProductsByAuthorName;

/// <summary>
///     GetProductsByAuthorName Handler
/// </summary>
public class GetProductsByAuthorNameHandler
    : IFeatureHandler<GetProductsByAuthorNameRequest, GetProductsByAuthorNameResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetProductsByAuthorNameHandler(IUnitOfWork unitOfWork)
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
    public async Task<GetProductsByAuthorNameResponse> HandlerAsync(
        GetProductsByAuthorNameRequest request,
        CancellationToken cancellationToken
    )
    {
        // Find products by author name trim.
        var products =
            await _unitOfWork.ProductFeature.GetProductsByAuthorNameRepository.FindProductsByAuthorNameQueryAsync(
                authorName: request.AuthorName,
                filterParameterQuery: new()
                {
                    SortField = request.SortField,
                    Order = request.Order,
                    PageIndex = request.PageIndex,
                    PageSize = request.PageSize,
                    MaxPrice = request.MaxPrice,
                    MinPrice = request.MinPrice,
                },
                cancellationToken: cancellationToken
            );

        // Get total number products.
        var countingProduct =
            await _unitOfWork.ProductFeature.GetProductsByAuthorNameRepository.GetTotalNumberOfProductsByAuthorNameQueryAsync(
                authorName: request.AuthorName.Trim(),
                cancellationToken: cancellationToken
            );

        // Response successfully.
        return new GetProductsByAuthorNameResponse()
        {
            StatusCode = GetProductsByAuthorNameResponseStatusCode.OPERATION_SUCCESS,
            ResponseBody = new()
            {
                Products = new PaginationResponse<GetProductsByAuthorNameResponse.Body.Product>()
                {
                    Contents = products.Select(
                        product => new GetProductsByAuthorNameResponse.Body.Product()
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
                    ),
                    PageIndex = request.PageIndex,
                    PageSize = request.PageSize,
                    TotalPages = (int)Math.Ceiling((double)countingProduct / request.PageSize)
                }
            }
        };
    }
}
