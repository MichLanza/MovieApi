namespace MovieApi.Dtos.Movies
{
    public class MovieFiltersDto : PaginationQuery
    {
        public string Title { get; set; } = string.Empty;

        public bool OnCinema { get; set; }

        public DateTime PremiereDate { get; set; }
    }
}
