using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace MovieApi.Dtos
{
    public class PagedList<T>
    {
        public int Page { get; }
        public int PageSize { get; }

        public int TotalCount { get; }

        public int TotalPages { get; }

        public List<T> Data { get; }

        public bool HasNextPage => Page * PageSize < TotalCount;
        public bool HasPrevious => PageSize > 1;

        public PagedList(int page, int pageSize, int totalCount, List<T> data, int totalPages)
        {
            Page = page;
            PageSize = pageSize;
            TotalCount = totalCount;
            Data = data;
            TotalPages = totalPages;
        }


        public static PagedList<T> Create(IEnumerable<T> query, int page, int pageSize)
        {
            int count = query.Count();
            int totalPages = (int)Math.Ceiling((double)count / pageSize);
            var data = query.Skip((page - 1) * pageSize)
                            .Take(pageSize).ToList();
            return new PagedList<T>(page, pageSize, count, data, totalPages);
        }
    }
}
