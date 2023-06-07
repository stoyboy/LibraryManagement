using LibraryManagement.Application.Infrastructure.Repositories;
using LibraryManagement.Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static System.Formats.Asn1.AsnWriter;

namespace LibraryManagement.Webapp.Pages.Employees
{
    [Authorize]
    public class DeleteModel : PageModel
    {
        private readonly EmployeeRepository _employees;
        public DeleteModel(EmployeeRepository employees)
        {
            _employees = employees;
        }

        [TempData]
        public string? Message { get; set; }
        public Employee Employee { get; set; } = default!;
        public IActionResult OnPostCancel() => RedirectToPage("/Employees/Index");
        public IActionResult OnPostDelete(Guid guid)
        {
            var employee = _employees.FindByGuid(guid);
            if (employee is null)
            {
                return RedirectToPage("/Employees/Index");
            }
            var (success, message) = _employees.Delete(employee);
            if (!success) { Message = message; }
            return RedirectToPage("/Employees/Index");
        }
        public IActionResult OnGet(Guid guid)
        {
            var employee = _employees.FindByGuid(guid);
            if (employee is null)
            {
                return RedirectToPage("/Employees/Index");
            }
            Employee = employee;
            return Page();
        }
    }
}
