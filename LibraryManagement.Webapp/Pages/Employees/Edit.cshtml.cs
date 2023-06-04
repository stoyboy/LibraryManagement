using AutoMapper;
using LibraryManagement.Application.Infrastructure;
using LibraryManagement.Application.Models;
using LibraryManagement.Webapp.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Webapp.Pages.Employees
{
    public class EditModel : PageModel
    {
        private readonly LibraryContext _db;
        private readonly IMapper _mapper;

        public IEnumerable<SelectListItem> RoleSelectList => _db.Roles.OrderBy(r => r.Name).Select(r => new SelectListItem(r.Name, r.Guid.ToString()));

        public EditModel(LibraryContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        [BindProperty]
        public EmployeeDto Employee { get; set; } = null!;

        public IActionResult OnPost(Guid guid)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var employee = _db.Employees.FirstOrDefault(e => e.Guid == guid);
            if (employee is null)
            {
                return RedirectToPage("/Employees/Index");
            }

            var role = _db.Roles.FirstOrDefault(r => r.Guid == Employee.RoleGuid);

            if (role is null)
            {
                return RedirectToPage("/Employees/Index");
            }

            _mapper.Map(Employee, employee);
            employee.Role = role;
            _db.Entry(employee).State = EntityState.Modified;

            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Fehler beim Schreiben in die Datenbank");
                return Page();
            }
            return RedirectToPage("/Employees/Index");
        }
        public IActionResult OnGet(Guid guid)
        {
            var employee = _db.Employees.FirstOrDefault(e => e.Guid == guid);
            if (employee is null)
            {
                return RedirectToPage("/Employees/Index");
            }
            Employee = _mapper.Map<EmployeeDto>(employee);
            return Page();
        }
    }
}
