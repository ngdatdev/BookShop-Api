using System.Collections.Generic;
using BookShop.Application.Shared.Features;

namespace BookShop.Application.Shared.Pagination;

/// <summary>
///     Represent the pagination response model.
/// </summary>
public class PaginationResponse<T>
{
    public IEnumerable<T> Contents { get; init; }

    public int PageIndex { get; init; } = 1;

    public int PageSize { get; init; } = 20;

    public int TotalPages { get; init; }

    public bool HasPreviousPage => PageIndex > 1;

    public bool HasNextPage => PageIndex < TotalPages;

    //public PaginationResponse(IEnumerable<T> contents, int pageIndex, int totalPages)
    //{
    //    Contents = contents;
    //    PageIndex = pageIndex;
    //    TotalPages = totalPages;
    //}
}
