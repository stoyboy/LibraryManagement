using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Webapp.Dto
{
    class ValidYear : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var book = validationContext.ObjectInstance as BookDto;
            if (book is null) { return null; }

            if (book.Year > DateTime.Now.Year)
                return new ValidationResult("Das Datum darf nicht in der Zukunft liegen.");

            return ValidationResult.Success;
        }
    }
    public record BookDto(
        Guid Guid,
        string? Title,
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Der Verlag muss zwischen 2 und 30 Zeichen lang sein.")]
        string Publisher,
        [ValidYear]
        int Year,
        [Range(0, 10, ErrorMessage = "Bewertung muss zwischen 0 und 10 Punkten sein.")]
        int Rating
        );
}
