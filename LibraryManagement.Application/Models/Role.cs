using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Models
{
    [Index(nameof(Name), IsUnique = true)]
    [Table("Roles")]
    public class Role
    {
        public int RoleId { get; private set; }
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public virtual List<Employee> Employees { get; set; } = new();

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        protected Role() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public Role(string name)
        {
            Guid = Guid.NewGuid();
            Name = name;
        }
    }
}
