using MovieApi.Dtos.NewFolder;

namespace MovieApi.Dtos.Movies
{
    public class ListMoviesDto
    {
        public List<MovieDto> NextPremiers { get; set; }
        public List<MovieDto> OnCinemas { get; set; }
    }
}
