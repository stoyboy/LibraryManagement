using AutoMapper;
using AutoMapper.QueryableExtensions;
using LibraryManagement.Application.Infrastructure;
using LibraryManagement.Application.Infrastructure.Repositories;
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
        private readonly AuthorRepository _author;
        private readonly BookRepository _books;
        private readonly IMapper _mapper;

        public DetailsModel(AuthorRepository author, BookRepository books, IMapper mapper)
        {
            _author = author;
            _books = books;
            _mapper = mapper;
        }

        [FromRoute]
        public Guid Guid { get; set; }
        public Author Author { get; private set; } = default!;
        public IReadOnlyList<Book> Books { get; private set; } = new List<Book>();
        public Dictionary<Guid, BookDto> EditBooks { get; set; } = new();
        public Dictionary<Guid, bool> BooksToDelete { get; set; } = new();

        public IActionResult OnGet(Guid guid)
        {
            var books = _books.FindByAuthorGuid(guid);
            if (books == null)
            {
                return RedirectToPage("/Authors/Index");
            }
            EditBooks = books
                .ProjectTo<BookDto>(_mapper.ConfigurationProvider)
                .ToDictionary(b => b.Guid, b => b);

            var author = _author.GetAuthorWithBooks(guid);
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

            var book = _books.FindByGuid(bookGuid);
            if (book == null)
            {
                return RedirectToPage();
            }
            
            _mapper.Map(editBooks[bookGuid], book);
            var (success, message) = _books.Update(book);

            if (!success)
            {
                ModelState.AddModelError("", message!);
                return Page();
            }

            return RedirectToPage();
        }

        public IActionResult OnPostDelete(Guid guid, Dictionary<Guid, bool> booksToDelete)
        {
            var booksDb = _books.Set.Where(o => o.Author.Guid == guid).ToDictionary(o => o.Guid, o => o);
            var bookGuids = booksToDelete.Where(o => o.Value == true).Select(o => o.Key);

            foreach (var o in bookGuids)
            {
                if (!booksDb.TryGetValue(o, out var offer))
                {
                    continue;
                }
                _books.Delete(offer);
            }
            return RedirectToPage();
        }

        public override void OnPageHandlerExecuting(PageHandlerExecutingContext context)
        {
            var author = _author.FindByGuid(Guid);
            if (author is null)
            {
                context.Result = RedirectToPage("/Authors/Index");
                return;
            }
            Books = _books.FindByAuthorGuid(Guid).ToList();
            BooksToDelete = Books.ToDictionary(o => o.Guid, o => false);
        }
    }
}
