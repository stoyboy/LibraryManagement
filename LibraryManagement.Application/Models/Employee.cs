﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Models
{
    [Table("Employees")]
    public class Employee : IEntity<int>
    {
        public int EmployeeId { get; private set; }
        public int Id => EmployeeId;
        public Guid Guid { get; private set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        protected Employee() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public Employee(string firstname, string lastname, Role role)
        {
            Guid = Guid.NewGuid();
            Firstname = firstname;
            Lastname = lastname;
            Role = role;
        }

        public static implicit operator string(Employee v)
        {
            throw new NotImplementedException();
        }
    }
}
