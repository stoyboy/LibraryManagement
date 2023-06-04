using AutoMapper;
using LibraryManagement.Application.Infrastructure;
using LibraryManagement.Webapp.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Webapp.Pages.Authors
{
    public class EditModel : PageModel
    {
        private readonly LibraryContext _db;
        private readonly IMapper _mapper;

        public EditModel(LibraryContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        [BindProperty]
        public AuthorDto Author { get; set; } = null!;

        public IActionResult OnPost(Guid guid)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var author = _db.Authors.FirstOrDefault(s => s.Guid == guid);
            if (author is null)
            {
                return RedirectToPage("/Authors/Index");
            }
            _mapper.Map(Author, author);
            _db.Entry(author).State = EntityState.Modified;
            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Fehler beim Schreiben in die Datenbank");
                return Page();
            }
            return RedirectToPage("/Authors/Index");
        }
        public IActionResult OnGet(Guid guid)
        {
            var store = _db.Authors.FirstOrDefault(s => s.Guid == guid);
            if (store is null)
            {
                return RedirectToPage("/Stores/Index");
            }
            Author = _mapper.Map<AuthorDto>(store);
            return Page();
        }
    }
}
