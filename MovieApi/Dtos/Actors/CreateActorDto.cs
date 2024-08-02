using MovieApi.Validations;
using System.ComponentModel.DataAnnotations;

namespace MovieApi.Dtos.Actors
{
    public class CreateActorDto : PatchActorDto
    {       

        [PhotoSizeValidation(maxSizeInMb:  4)]
        [FileTypeValidation(fileType: FileType.Image)]
        public IFormFile? Photo { get; set; }
    }
}
