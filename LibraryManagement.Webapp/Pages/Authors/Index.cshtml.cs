using Bogus.DataSets;
using LibraryManagement.Application.Infrastructure;
using LibraryManagement.Application.Infrastructure.Repositories;
using LibraryManagement.Application.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryManagement.Webapp.Pages.Books
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class IndexModel : PageModel
    {
        private readonly AuthorRepository _authors;
        [TempData]
        public string? Message { get; set; }
        public IReadOnlyList<AuthorRepository.AuthorWithBookCount> Authors { get; private set; } = new List<AuthorRepository.AuthorWithBookCount>();
        public IndexModel(AuthorRepository authors)
        {
            _authors = authors;
        }

        public IActionResult OnGet()
        {
            Authors = _authors.GetAuthorsWithBookCount();
            return Page();
        }
    }
}
