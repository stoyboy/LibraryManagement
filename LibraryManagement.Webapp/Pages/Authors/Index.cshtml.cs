using Bogus.DataSets;
using LibraryManagement.Application.Infrastructure;
using LibraryManagement.Application.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryManagement.Webapp.Pages.Books
{
    public class IndexModel : PageModel
    {
        private readonly AuthorRepository _authors;
        public IReadOnlyList<AuthorRepository.AuthorWithBookCount> Authors { get; private set; } = new List<AuthorRepository.AuthorWithBookCount>();
        public IndexModel(AuthorRepository authors)
        {
            _authors = authors;
        }

        public void OnGet()
        {
            Authors = _authors.GetAuthorsWithBookCount();
        }
    }
}
