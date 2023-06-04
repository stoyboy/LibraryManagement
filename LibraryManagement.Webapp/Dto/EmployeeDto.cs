using LibraryManagement.Application.Models;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Webapp.Dto
{
    public record EmployeeDto(
        Guid Guid,
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Der Vorname muss zwischen 2 und 30 Zeichen lang sein.")]
        string Firstname,
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Der Vorname muss zwischen 2 und 30 Zeichen lang sein.")]
        string Lastname,
        Guid RoleGuid
        );
}
