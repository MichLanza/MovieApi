namespace MovieApi.Dtos
{
    public class Pagination
    {
        public int Page { get; set; } = 1;
        private int pageSize = 0;

        public int PageSize
        {
            get => pageSize;
            set
            {
                //pageSize = (value > 50) ? 50 : value;   
                pageSize = value;
            }

        }

    }
}
