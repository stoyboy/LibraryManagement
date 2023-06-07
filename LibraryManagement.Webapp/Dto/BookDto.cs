using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Webapp.Dto
{
    public record BookDto(
        Guid Guid,
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Der Verlag muss zwischen 2 und 30 Zeichen lang sein.")]
        string? Title,
        string Publisher,
        int Year,
        [Range(0, 10, ErrorMessage = "Bewertung muss zwischen 0 und 10 Punkten sein.")]
        int Rating
        );
}
