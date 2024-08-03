using Microsoft.EntityFrameworkCore;

namespace MovieApi.Extensions
{
    public static class HttpContextExtensions
    {
        public async static Task InsertPaging<T>(this HttpContext httpContext, IQueryable<T> queryable, int countPerPage)
        {
            double count = await queryable.CountAsync();
            double totalPages = Math.Ceiling(count / countPerPage);
            httpContext.Response.Headers.Add("Total", totalPages.ToString());
        }
    }
}
