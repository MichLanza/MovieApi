using System.ComponentModel.DataAnnotations;

namespace MovieApi.Dtos.Actors
{
    public class CreateActorDto
    {
        [Required]
        [StringLength(170)]
        public string Name { get; set; } = string.Empty;

        public DateTime BirthDay { get; set; }

        public IFormFile Photo { get; set; }
    }
}
