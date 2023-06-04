using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Models
{
    [Table("Authors")]
    public class Author
    {
        public int AuthorId { get; private set; }
        public Guid Guid { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime BirthDate { get; set; }
        public string Nationality { get; set; }
        public virtual List<Book> Books { get; set; } = new ();

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        protected Author() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public Author(string firstname, string lastname, DateTime birthDate, string nationality)
        {
            Guid = Guid.NewGuid();
            Firstname = firstname;
            Lastname = lastname;
            BirthDate = birthDate;
            Nationality = nationality;
        }
    }
}
