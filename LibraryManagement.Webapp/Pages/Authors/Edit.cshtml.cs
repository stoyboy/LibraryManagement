using AutoMapper;
using LibraryManagement.Application.Infrastructure;
using LibraryManagement.Application.Infrastructure.Repositories;
using LibraryManagement.Webapp.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Webapp.Pages.Authors
{
    public class EditModel : PageModel
    {
        private readonly AuthorRepository _author;
        private readonly IMapper _mapper;

        public EditModel(AuthorRepository author, IMapper mapper)
        {
            _author = author;
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

            var author = _author.FindByGuid(guid);
            if (author is null)
            {
                return RedirectToPage("/Authors/Index");
            }
            _mapper.Map(Author, author);
            var (success, message) = _author.Update(author);

            if (!success)
            {
                ModelState.AddModelError("", message!);
                return Page();
            }
            return RedirectToPage("/Authors/Index");
        }

        public IActionResult OnGet(Guid guid)
        {
            var author = _author.FindByGuid(guid);
            if (author is null)
            {
                return RedirectToPage("/Authors/Index");
            }
            Author = _mapper.Map<AuthorDto>(author);
            return Page();
        }
    }
}
