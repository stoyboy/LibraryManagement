using AutoMapper;
using LibraryManagement.Application.Infrastructure;
using LibraryManagement.Application.Infrastructure.Repositories;
using LibraryManagement.Application.Models;
using LibraryManagement.Webapp.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Webapp.Pages.Employees
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly EmployeeRepository _employees;
        private readonly RoleRepository _roles;
        private readonly IMapper _mapper;

        public IEnumerable<SelectListItem> RoleSelectList => _roles.Set.OrderBy(r => r.Name).Select(r => new SelectListItem(r.Name, r.Guid.ToString()));

        public EditModel(EmployeeRepository employees, RoleRepository roles, IMapper mapper)
        {
            _employees = employees;
            _roles = roles;
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

            var employee = _employees.FindByGuid(guid);
            if (employee is null)
            {
                return RedirectToPage("/Employees/Index");
            }

            _mapper.Map(Employee, employee);
            var (success, message) = _employees.Update(employee, Employee.RoleGuid);

            if (!success)
            {
                ModelState.AddModelError("", message!);
                return Page();
            }
            return RedirectToPage("/Employees/Index");
        }
        public IActionResult OnGet(Guid guid)
        {
            var employee = _employees.FindByGuid(guid);
            if (employee is null)
            {
                return RedirectToPage("/Employees/Index");
            }
            Employee = _mapper.Map<EmployeeDto>(employee);
            return Page();
        }
    }
}
