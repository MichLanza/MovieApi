using System.ComponentModel.DataAnnotations;

namespace MovieApi.Validations
{
    public partial class PhotoSizeValidation : ValidationAttribute
    {

        private readonly int MaxSizeInMb;

        public PhotoSizeValidation(int maxSizeInMb)
        {
            MaxSizeInMb = maxSizeInMb;
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

            var size = MaxSizeInMb * 1024 * 1024;

            if (formFile.Length > size)
            {
                return new ValidationResult($"El peso de la imagen no debe ser mayor a {MaxSizeInMb}mb");
            }


            return ValidationResult.Success;


        }

    }

}
