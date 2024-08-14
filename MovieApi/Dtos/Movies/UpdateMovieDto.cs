using Microsoft.AspNetCore.Mvc;
using MovieApi.Helpers;
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

        [ModelBinder(BinderType = typeof(TypeBinder<List<int>>))]
        public List<int> GenreIds { get; set; }

        [ModelBinder(BinderType = typeof(TypeBinder<List<ActorMovieCreateDto>>))]
        public List<ActorMovieCreateDto> Actors { get; set; }
    }

}
