using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Models
{
    [Table("Books")]
    public class Book
    {
        public int BookId { get; private set; }
        public Guid Guid { get; set; }
        public string Title { get; set; }
        public int AuthorId { get; set; }
        public virtual Author Author { get; set; }
        public string Publisher { get; set; }
        public int Year { get; set; }
        public int? BorrowId { get; set; }
        public virtual Borrow? Borrow { get; set; }
        public int Rating { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        protected Book() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public Book(string title, Author author, string publisher, int year, int rating)
        {
            Guid = Guid.NewGuid();
            Title = title;
            Author = author;
            Publisher = publisher;
            Year = year;
            Rating = rating;    
        }
    }
}
