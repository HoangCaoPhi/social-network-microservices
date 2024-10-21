using Application.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Extensions;

public static class QueryableExtensions
{
    public static async Task<PagedList<T>> PagingAsync<T>(this IQueryable<T> query, PagingModel pagingModel, CancellationToken cancellationToken = default)
    {
        var page = pagingModel.PageNumber ?? 1;
        var size = pagingModel.PageSize ?? 1;

        var totalCount = await query.CountAsync(cancellationToken: cancellationToken);
        var items = await query
            .Skip((page - 1) * size)
            .Take(size)
            .ToListAsync(cancellationToken);

        return new PagedList<T>(
            page,
            size,
            totalCount,
            items);
    }
}
