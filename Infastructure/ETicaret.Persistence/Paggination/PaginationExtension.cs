using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace ETicaret.Persistence.Paggination;

public static class PaginationExtension
{
    public async static Task<PaginationResponse<T>> Where<T>(this IQueryable<T> queryable,
        PaginationRequest paginationRequest, Expression<Func<T, object>> orderByExpression)
    {
        var list = await queryable.OrderBy(orderByExpression).Skip((paginationRequest.CurrentPage - 1) * paginationRequest.size)
            .Take(paginationRequest.size)
            .ToListAsync();
        var count = await queryable.CountAsync();
        var pagination = new PaginationResponse<T>(
            list,
            paginationRequest.CurrentPage,
            paginationRequest.size,
            count);
        return pagination;
    }
}