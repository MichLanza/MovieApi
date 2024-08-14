using MovieApi.Dtos.Movies;

namespace MovieApi.Dtos.Movies
{
    public class ListMoviesDto
    {
        public List<MovieDto> NextPremiers { get; set; }
        public List<MovieDto> OnCinemas { get; set; }
    }
}
