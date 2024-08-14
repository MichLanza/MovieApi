using System.ComponentModel.DataAnnotations;

namespace MovieApi.Dtos.Movies
{
    public class MovieDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; } = string.Empty;

        public bool OnCinema { get; set; }

        public DateTime PremiereDate { get; set; }

        public string Poster { get; set; } = string.Empty;
    }
}
