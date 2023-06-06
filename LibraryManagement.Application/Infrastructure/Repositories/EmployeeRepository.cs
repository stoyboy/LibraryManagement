using LibraryManagement.Application.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Infrastructure.Repositories
{
    public class EmployeeRepository : Repository<Employee, int>
    {
        public EmployeeRepository(LibraryContext db) : base(db)
        {
        }

        public (bool success, string? message) Insert(string firstname, string lastname, Guid roleGuid)
        {
            var role = _db.Roles.FirstOrDefault(r => r.Guid == roleGuid);
            if (role == null) { return (false, "Ungültige Rolle."); }

            return base.Insert(new Employee(firstname, lastname, role));
        }

        public (bool success, string? message) Update(Employee employee, Guid roleGuid)
        {
            var role = _db.Roles.FirstOrDefault(r => r.Guid == roleGuid);
            if (role == null) { return (false, "Ungültige Rolle."); }

            employee.Role = role;
            return base.Update(employee);
        }

        public Employee? FindByGuid(Guid guid) => _db.Set<Employee>().Include(e => e.Role).FirstOrDefault(e => e.Guid.Equals(guid));
    }
}
