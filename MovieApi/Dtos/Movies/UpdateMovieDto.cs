using MovieApi.Validations;

namespace MovieApi.Dtos.Movies
{
    public class UpdateMovieDto
    {
        public string Title { get; set; } = string.Empty;

        public bool OnCinema { get; set; }

        public DateTime PremiereDate { get; set; }

        [PhotoSizeValidation(maxSizeInMb: 4)]
        [FileTypeValidation(fileType: FileType.Image)]
        public IFormFile Poster { get; set; }
    }

}
