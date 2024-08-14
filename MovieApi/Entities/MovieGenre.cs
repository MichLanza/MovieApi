namespace MovieApi.Entities
{
    public class MovieGenre
    {
        public Guid IdMovie { get; set; }
        public Guid IdGenre { get; set; }
        public Genre Genre { get; set; }
        public Movie Movie { get; set; }
    }
}
