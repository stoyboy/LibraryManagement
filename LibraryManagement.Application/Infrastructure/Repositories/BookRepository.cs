using LibraryManagement.Application.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Infrastructure.Repositories
{
    public class BookRepository : Repository<Book, int>
    {
        public BookRepository(LibraryContext db) : base(db)
        {
        }

        public IQueryable<Book> FindByAuthorGuid(Guid guid)
        {
            return _db.Books.Include(b => b.Author).Where(b => b.Author.Guid == guid);
        }
    }
}
