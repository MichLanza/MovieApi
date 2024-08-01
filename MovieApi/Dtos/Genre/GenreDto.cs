using System.ComponentModel.DataAnnotations;

namespace MovieApi.Dtos.Genre
{
    public class GenreDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(40)]
        public string Name { get; set; } = string.Empty;
    }
}
