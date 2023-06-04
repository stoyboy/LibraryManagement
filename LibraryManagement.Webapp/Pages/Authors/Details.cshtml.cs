using AutoMapper;
using AutoMapper.QueryableExtensions;
using LibraryManagement.Application.Infrastructure;
using LibraryManagement.Application.Models;
using LibraryManagement.Webapp.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;

namespace LibraryManagement.Webapp.Pages.Books
{
    public class DetailsModel : PageModel
    {
        private readonly LibraryContext _db;
        private readonly IMapper _mapper;

        public DetailsModel(LibraryContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        [FromRoute]
        public Guid Guid { get; set; }
        public Author Author { get; private set; } = default!;
        public IReadOnlyList<Book> Books { get; private set; } = new List<Book>();
        public Dictionary<Guid, BookDto> EditBooks { get; set; } = new();

        public IActionResult OnGet(Guid guid)
        {
            EditBooks = _db.Books.Where(b => b.Author.Guid == Guid)
                .ProjectTo<BookDto>(_mapper.ConfigurationProvider)
                .ToDictionary(b => b.Guid, b => b);

            var author = _db.Authors.Include(b => b.Books).ThenInclude(b => b.Borrow).ThenInclude(b => b.Member).FirstOrDefault(a => a.Guid == guid);

            if (author == null)
            {
                return RedirectToPage("/Authors/Index");
            }

            Author = author;
            return Page();
        }

        public IActionResult OnPostEditBook(Guid guid, Guid bookGuid, Dictionary<Guid, BookDto> editBooks)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToPage();
            }

            var book = _db.Books.FirstOrDefault(b => b.Guid == bookGuid);
            if (book == null)
            {
                return RedirectToPage();
            }
            
            _mapper.Map(editBooks[bookGuid], book);
            _db.Entry(book).State = EntityState.Modified;

            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Fehler beim Schreiben in die Datenbank.");
                return Page();
            }

            return RedirectToPage();
        }

        public override void OnPageHandlerExecuting(PageHandlerExecutingContext context)
        {
            var author = _db.Authors
                .FirstOrDefault(a => a.Guid == Guid);
            if (author is null)
            {
                context.Result = RedirectToPage("/Authors/Index");
                return;
            }
            Books = _db.Books.Include(b => b.Author).Where(a => a.Guid == Guid).ToList();
        }
    }
}
