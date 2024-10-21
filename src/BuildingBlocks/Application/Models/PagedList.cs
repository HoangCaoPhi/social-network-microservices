namespace Application.Models;

public sealed class PagedList<T>(
    int Page,
    int PageSize,
    int TotalCount,
    IReadOnlyCollection<T> Items)
{
    public int Page { get; } = Page;

    public int PageSize { get; } = PageSize;

    public int TotalCount { get; } = TotalCount;

    public int TotalPages { get; } = (int)Math.Ceiling(TotalCount / (double)PageSize);

    public bool HasNextPage => Page * PageSize < TotalCount;

    public bool HasPreviousPage => Page > 1;

    public IReadOnlyCollection<T> Items { get; } = Items;
}