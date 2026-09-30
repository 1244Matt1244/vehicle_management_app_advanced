namespace VehicleApp.Application.Common.Models;

public class PaginatedList<T>
{
    public List<T> Items { get; init; } = new();
    public int PageIndex { get; init; }
    public int TotalPages { get; init; }
    public int TotalCount { get; init; }

    public bool HasPreviousPage => PageIndex > 1;
    public bool HasNextPage => PageIndex < TotalPages;

    public static PaginatedList<T> Create(List<T> items, int count, int pageIndex, int pageSize)
        => new()
        {
            Items = items,
            TotalCount = count,
            PageIndex = pageIndex,
            TotalPages = (int)Math.Ceiling(count / (double)pageSize)
        };
}
