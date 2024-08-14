namespace MovieApi.Entities
{
    public class MovieGenre
    {
        public Guid MovieId { get; set; }
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
        public Movie Movie { get; set; }
    }
}
