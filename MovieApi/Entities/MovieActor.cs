namespace MovieApi.Entities
{
    public class MovieActor
    {
        public Guid MovieId { get; set; }
        public Guid ActorId { get; set; }
        public string Character { get; set; }
        public Actor Actor { get; set; }
        public Movie Movie { get; set; }


    }
}
