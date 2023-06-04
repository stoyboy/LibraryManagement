using AutoMapper;
using LibraryManagement.Application.Infrastructure;
using LibraryManagement.Application.Models;
using LibraryManagement.Webapp.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;

namespace LibraryManagement.Webapp.Pages.Employees
{
    public class IndexModel : PageModel
    {
        private readonly LibraryContext _db;
        private readonly IMapper _mapper;

        public IEnumerable<SelectListItem> RoleSelectList => _db.Roles.OrderBy(r => r.Name).Select(r => new SelectListItem(r.Name, r.Guid.ToString()));

        public record EmployeeWithoutId(
                Guid Guid,
                string Firstname,
                string Lastname,
                string Role
            );

        [FromRoute]
        public Guid Guid { get; set; }

        [BindProperty]
        public EmployeeDto NewEmployee { get; set; } = default!;
        
        public List<EmployeeWithoutId> Employees { get; private set; } = new();

        public IndexModel(LibraryContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public void OnGet()
        {
            Employees = _db.Employees.Include(e => e.Role).Select(e => new EmployeeWithoutId(
                    e.Guid,
                    e.Firstname,
                    e.Lastname,
                    e.Role.Name
                ))
                .ToList();
        }

        public IActionResult OnPostNewEmployee(Guid guid)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                var employee = _mapper.Map<Employee>(NewEmployee);

                employee.Role = _db.Roles.FirstOrDefault(r => r.Guid == NewEmployee.RoleGuid)
                    ?? throw new ApplicationException("Ungültige Rolle.");

                _db.Employees.Add(employee);
                _db.SaveChanges();
            }
            catch (ApplicationException e)
            {
                ModelState.AddModelError("", e.Message);
                return Page();
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Fehler beim Schreiben in die Datenbank.");
                return Page();
            }
            return RedirectToPage();
        }
    }
}
