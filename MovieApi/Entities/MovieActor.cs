namespace MovieApi.Entities
{
    public class MovieActor
    {
        public Guid IdMovie { get; set; }
        public Guid IdActor { get; set; }
        public string Character { get; set; }
        public Actor Actor { get; set; }
        public Movie Movie { get; set; }


    }
}
