namespace BookShop.Data.Shared.FilterAndPagination;

/// <summary>
///     FilterParameterQuery for parameter of repository.
/// </summary>
public class FilterParameterQuery
{
    public int PageIndex { get; init; }

    public int PageSize { get; init; }

    public string SortField { get; init; }

    public string Order { get; init; }

    public decimal? MinPrice { get; set; }

    public decimal? MaxPrice { get; set; }
}
