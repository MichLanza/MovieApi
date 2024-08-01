using System.ComponentModel.DataAnnotations;

namespace MovieApi.Dtos.Genre
{
    public class CreateGenreDto
    {
        [Required]
        [StringLength(40)]
        public string Name { get; set; } = string.Empty;
    }
}
