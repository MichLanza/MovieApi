using MovieApi.Validations;

namespace MovieApi.Dtos.Movies
{
    public class CreateMovieDto : PatchMovieDto
    {   

        [PhotoSizeValidation(maxSizeInMb: 4)]
        [FileTypeValidation(fileType: FileType.Image)]
        public IFormFile Poster { get; set; }


        public List<Guid> GenreIds { get; set; }

    }

}
