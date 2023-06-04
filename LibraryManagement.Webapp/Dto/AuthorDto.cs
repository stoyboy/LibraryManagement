using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Webapp.Dto
{
    public record AuthorDto(
        Guid Guid,
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Der Vorname muss zwischen 2 und 30 Zeichen lang sein.")]
        string Firstname,
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Der Vorname muss zwischen 2 und 30 Zeichen lang sein.")]
        string Lastname,
        DateTime BirthDate,
        [StringLength(70, MinimumLength = 2, ErrorMessage = "Die Nationalität muss zwischen 2 und 70 Zeichen lang sein.")]
        string Nationality
        );
}
