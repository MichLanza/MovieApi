using System.ComponentModel.DataAnnotations;

namespace MovieApi.Dtos.Actors
{
    public class ActorDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(170)]
        public string Name { get; set; } = string.Empty;

        public DateTime BirthDay { get; set; }

        public string Photo { get; set; } = string.Empty;
    }
}
