using AutoMapper;
using LibraryManagement.Application.Infrastructure;
using LibraryManagement.Application.Infrastructure.Repositories;
using LibraryManagement.Application.Models;
using LibraryManagement.Webapp.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;

namespace LibraryManagement.Webapp.Pages.Employees
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly EmployeeRepository _employees;
        private readonly RoleRepository _roles;

        public IEnumerable<SelectListItem> RoleSelectList => _roles.Set.OrderBy(r => r.Name).Select(r => new SelectListItem(r.Name, r.Guid.ToString()));

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
        
        public List<Employee> Employees { get; private set; } = new();

        public IndexModel(EmployeeRepository employees, RoleRepository roles)
        {
            _employees = employees;
            _roles = roles;
        }

        public IActionResult OnGet()
        {
            var employees = _employees.Set.Include(e => e.Role).ToList();
            if (employees == null)
            {
                return RedirectToPage("/Employees/Index");
            }

            Employees = employees;
            return Page();
        }

        public IActionResult OnPostNewEmployee(Guid guid)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var (success, message) = _employees.Insert(NewEmployee.Firstname, NewEmployee.Lastname, NewEmployee.RoleGuid);

            if (!success)
            {
                ModelState.AddModelError("", message!);
                return Page();
            }

            return RedirectToPage();
        }
    }
}
