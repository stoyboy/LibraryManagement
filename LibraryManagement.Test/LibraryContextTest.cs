using LibraryManagement.Application.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Test
{
    public class LibraryContextTest
    {
        private LibraryContext GetDatabase(bool deleteDb = false)
        {
            var db = new LibraryContext(new DbContextOptionsBuilder().UseSqlite("Data Source=library.db").UseLazyLoadingProxies().Options);

            if (deleteDb)
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
            }

            return db;
        }

        [Fact]
        public void CreateDatabaseSuccessTest()
        {
            using var db = GetDatabase(deleteDb: true);
        }

        [Fact]
        public void SeedDatabaseTest()
        {
            using var db = GetDatabase(deleteDb: true);
            db.Seed();
        }
    }
}
