using MovieApi.Dtos.Genre;

namespace MovieApi.Dtos.Movies
{
    public class MovieDetailDto : MovieDto
    {
        public List<GenreDto> Genres { get; set; }

        public List<ActorMovieDetailDto> Actors { get; set; }

    }
}
