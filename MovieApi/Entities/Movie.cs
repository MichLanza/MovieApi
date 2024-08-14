using System.ComponentModel.DataAnnotations;

namespace MovieApi.Entities
{
    public class Movie
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(300)]
        public string Title { get; set; } = string.Empty;

        public bool OnCinema { get; set; }

        public DateTime PremiereDate { get; set; }

        public string Poster { get; set; } = string.Empty;

        public List<MovieActor> MovieActors { get; set; }
        public List<MovieGenre> MovieGenre { get; set; }


    }
}
