using System.ComponentModel.DataAnnotations;

namespace MovieApi.Validations
{
    public class FileTypeValidation : ValidationAttribute
    {

        private readonly string[] ValidTypes;

        public FileTypeValidation(string[] validTypes)
        {
            ValidTypes = validTypes;
        }

        public FileTypeValidation(FileType fileType)
        {
            if (fileType == FileType.Image)
            {
                ValidTypes = new string[] { "image/jpeg", "image/png", "image/gif" };
            }

        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            IFormFile formFile = value as IFormFile;

            if (formFile == null)
            {
                return ValidationResult.Success;
            }


            if (!ValidTypes.Contains(formFile?.ContentType))
            {
                return new ValidationResult($"el tipo del archivo debe ser alguno de los siguientes: {string.Join(',', ValidTypes)}");
            }


            return ValidationResult.Success;


        }

    }

}
