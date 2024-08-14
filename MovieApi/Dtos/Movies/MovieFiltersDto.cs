namespace MovieApi.Dtos.Movies
{
    public class MovieFiltersDto : PaginationQuery
    {
        public string Title { get; set; } = string.Empty;

        public bool OnCinema { get; set; }

        public bool PremiereDate { get; set; }

        public int Genre { get; set; } = 0;

        public string ColumnToSort { get; set; } = string.Empty ;

        public bool AscOrder { get; set; } = true;

    }
}
