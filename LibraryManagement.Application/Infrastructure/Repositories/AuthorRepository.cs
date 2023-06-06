using LibraryManagement.Application.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Infrastructure.Repositories
{
    public class AuthorRepository : Repository<Author, int>
    {
        public record AuthorWithBookCount(
                Guid Guid,
                string Firstname,
                string Lastname,
                DateTime BirthDate,
                string Nationality,
                int BookCount
            );

        public AuthorRepository(LibraryContext db) : base(db)
        {
        }

        public IReadOnlyList<AuthorWithBookCount> GetAuthorsWithBookCount()
        {
            return _db.Authors
                .Select(a => new AuthorWithBookCount(a.Guid, a.Firstname, a.Lastname, a.BirthDate, a.Nationality, a.Books.Count))
                .ToList();
        }

        public Author? GetAuthorWithBooks(Guid guid)
        {
            return _db.Authors
                .Include(a => a.Books)
                .ThenInclude(b => b.Borrow)
                .ThenInclude(b => b.Member)
                .FirstOrDefault(a => a.Guid == guid);
        }

        public override (bool success, string? message) Delete(Author entity)
        {
            var books = _db.Books.Where(b => b.Author.Guid == entity.Guid).ToList();
            if (books.Count > 0) { return (false, $"Dem Autor {entity.Firstname} {entity.Lastname} sind noch Bücher zugewiesen."); }
            return base.Delete(entity);
        }
    }
}
