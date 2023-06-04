using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Models
{
    [Table("Borrows")]
    public class Borrow
    {
        public int BorrowId { get; private set; }
        public Guid Guid { get; set; }
        public int BookId { get; set; }
        public virtual Book Book { get; set; }
        public int MemberId { get; set; }
        public virtual Member Member { get; set; }
        public DateTime BorrowOutDate { get; set; } = DateTime.Now;
        public DateTime ReturnDate { get; set; } = DateTime.Now.AddDays(14);

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        protected Borrow() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public Borrow(Book book, Member member)
        {
            Guid = Guid.NewGuid();
            Book = book;
            Member = member;
        }
    }
}
