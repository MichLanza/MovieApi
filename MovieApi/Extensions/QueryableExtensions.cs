using MovieApi.Dtos;

namespace MovieApi.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> Paging<T>(this IQueryable<T> queryable , Pagination pagination)
        {
            return queryable
                .Skip((pagination.Page-1) * pagination.PageSize)
                .Take(pagination.PageSize);
        }
    }
}
