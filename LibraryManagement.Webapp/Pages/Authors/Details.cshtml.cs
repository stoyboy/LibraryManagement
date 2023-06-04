using LibraryManagement.Application.Infrastructure;
using LibraryManagement.Application.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Webapp.Pages.Books
{
    public class DetailsModel : PageModel
    {
        private readonly LibraryContext _db;
        public DetailsModel(LibraryContext db)
        {
            _db = db;
        }

        public Author Author { get; private set; } = default!;
        public IActionResult OnGet(Guid guid)
        {
            var author = _db.Authors.Include(b => b.Books).ThenInclude(b => b.Borrow).ThenInclude(b => b.Member).FirstOrDefault(t => t.Guid == guid);

            if (author == null)
            {
                return RedirectToPage("/Authors/Index");
            }

            Author = author;
            return Page();
        }
    }
}
