using Bogus.DataSets;
using LibraryManagement.Application.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryManagement.Webapp.Pages.Books
{
    public class IndexModel : PageModel
    {
        public record AuthorWithBookCount(
                Guid Guid,
                string Firstname,
                string Lastname,
                DateTime BirthDate,
                string Nationality,
                int BookCount
            );

        private readonly LibraryContext _db;
        public List<AuthorWithBookCount> Authors { get; private set; } = new();
        public IndexModel(LibraryContext db)
        {
            _db = db;
        }

        public void OnGet()
        {
            Authors = _db.Authors.Select(b => new AuthorWithBookCount(
                    b.Guid,
                    b.Firstname,
                    b.Lastname,
                    b.BirthDate,
                    b.Nationality,
                    b.Books.Count
                ))
                .ToList();
        }
    }
}
