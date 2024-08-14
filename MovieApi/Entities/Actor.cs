using System.ComponentModel.DataAnnotations;

namespace MovieApi.Entities
{
    public class Actor
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(170)]
        public string Name { get; set; } = string.Empty;

        public DateTime BirthDay { get; set; } 

        public string Photo { get; set; } = string.Empty;
        public List<MovieActor> MovieActors { get; set; }

    }
}
