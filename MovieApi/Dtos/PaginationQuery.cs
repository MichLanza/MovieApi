namespace MovieApi.Dtos
{
    public class PaginationQuery
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 0;
        //private int pageSize = 0;

        //public int PageSize
        //{
        //    get => pageSize;
        //    set
        //    {
        //        pageSize = (value > 50) ? 50 : value;   
        //    }

    }

}

