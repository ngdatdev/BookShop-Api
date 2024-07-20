using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Features;
using BookShop.Application.Shared.Pagination;
using BookShop.Data.Features.UnitOfWork;

namespace BookShop.Application.Features.Product.SearchProductsByKeyword;

/// <summary>
///     SearchProductsByKeyword Handler
/// </summary>
public class SearchProductsByKeywordHandler
    : IFeatureHandler<SearchProductsByKeywordRequest, SearchProductsByKeywordResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public SearchProductsByKeywordHandler(IUnitOfWork unitOfWork)
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
    public async Task<SearchProductsByKeywordResponse> HandlerAsync(
        SearchProductsByKeywordRequest request,
        CancellationToken cancellationToken
    )
    {

        // Find all products by keyword
        var products =
            await _unitOfWork.ProductFeature.SearchProductsByKeywordRepository.FindProductsByKeywordQueryAsync(
                keyword: request.Keyword,
                similarityThreshold: 0.3,
                filterParameterQuery: new Data.Shared.FilterAndPagination.FilterParameterQuery()
                {
                    SortField = request.SortField,
                    Order = request.Order,
                    PageIndex = request.PageIndex,
                    PageSize = request.PageSize,
                    MaxPrice = request.MaxPrice,
                    MinPrice = request.MinPrice,
                    Filters = request.Filters,
                },
                cancellationToken: cancellationToken
            );

        // Get total number products.
        var countProduct =
            await _unitOfWork.ProductFeature.SearchProductsByKeywordRepository.GetTotalNumberOfProductsByKeywordQueryAsync(
                keyword: request.Keyword,
                cancellationToken: cancellationToken
            );

        // Response successfully.
        return new SearchProductsByKeywordResponse()
        {
            StatusCode = SearchProductsByKeywordResponseStatusCode.OPERATION_SUCCESS,
            ResponseBody = new()
            {
                Products = new PaginationResponse<SearchProductsByKeywordResponse.Body.Product>()
                {
                    Contents = products.Select(
                        product => new SearchProductsByKeywordResponse.Body.Product()
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
                    TotalPages = (int)Math.Ceiling((double)countProduct / request.PageSize)
                },
               
            }
        };
    }
}
