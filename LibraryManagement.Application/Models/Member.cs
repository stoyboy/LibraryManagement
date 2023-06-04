using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Models
{
    [Table("Members")]
    public class Member
    {
        public int MemberId { get; private set; }
        public Guid Guid { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public bool IsAdmin { get; set; }
        public virtual List<Borrow> Borrows { get; set; } = new ();

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        protected Member() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public Member(string firstname, string lastname, bool isAdmin)
        {
            Guid = Guid.NewGuid();
            Firstname = firstname;
            Lastname = lastname;
            IsAdmin = isAdmin;
        }
    }
}
