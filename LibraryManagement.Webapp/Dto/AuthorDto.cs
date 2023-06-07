using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Webapp.Dto
{
    class ValidBirthDate : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var author = validationContext.ObjectInstance as AuthorDto;
            if (author is null) { return null; }

            if (author.BirthDate > DateTime.Now.AddYears(-18))
                return new ValidationResult("Der Autor muss mindestens 18 Jahre alt sein.");

            return ValidationResult.Success;
        }
    }

    public record AuthorDto(
        Guid Guid,
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Der Vorname muss zwischen 2 und 30 Zeichen lang sein.")]
        string Firstname,
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Der Vorname muss zwischen 2 und 30 Zeichen lang sein.")]
        string Lastname,
        [ValidBirthDate]
        DateTime BirthDate,
        [StringLength(70, MinimumLength = 2, ErrorMessage = "Die Nationalität muss zwischen 2 und 70 Zeichen lang sein.")]
        string Nationality
        );
}
