using LibraryManagement.Application.Infrastructure.Repositories;
using LibraryManagement.Application.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static System.Formats.Asn1.AsnWriter;

namespace LibraryManagement.Webapp.Pages.Authors
{
    public class DeleteModel : PageModel
    {
        private readonly AuthorRepository _authors;
        public DeleteModel(AuthorRepository authors)
        {
            _authors = authors;
        }

        [TempData]
        public string? Message { get; set; }
        public Author Author { get; set; } = default!;
        public IActionResult OnPostCancel() => RedirectToPage("/Authors/Index");
        public IActionResult OnPostDelete(Guid guid)
        {
            var author = _authors.FindByGuid(guid);
            if (author is null)
            {
                return RedirectToPage("/Authors/Index");
            }
            var (success, message) = _authors.Delete(author);
            if (!success) { Message = message; }
            return RedirectToPage("/Authors/Index");
        }
        public IActionResult OnGet(Guid guid)
        {
            var author = _authors.FindByGuid(guid);
            if (author is null)
            {
                return RedirectToPage("/Authors/Index");
            }
            Author = author;
            return Page();
        }
    }
}
