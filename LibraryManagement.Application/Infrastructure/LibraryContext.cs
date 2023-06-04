using Bogus;
using LibraryManagement.Application.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Infrastructure
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions options) : base(options) { }

        public DbSet<Book> Books => Set<Book>();
        public DbSet<Author> Authors => Set<Author>();
        public DbSet<Member> Members => Set<Member>();
        public DbSet<Employee> Employees => Set<Employee>();
        public DbSet<Borrow> Borrows => Set<Borrow>();
        public DbSet<Role> Roles => Set<Role>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=library.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>().HasMany(a => a.Books);
            modelBuilder.Entity<Author>().HasAlternateKey(a => a.Guid);
            modelBuilder.Entity<Author>().Property(a => a.Guid).ValueGeneratedOnAdd();

            modelBuilder.Entity<Book>().HasOne(b => b.Author);
            modelBuilder.Entity<Book>().HasOne(b => b.Borrow);
            modelBuilder.Entity<Book>().HasAlternateKey(b => b.Guid);
            modelBuilder.Entity<Book>().Property(b => b.Guid).ValueGeneratedOnAdd();

            modelBuilder.Entity<Borrow>().HasOne(b => b.Book).WithOne(b => b.Borrow).HasForeignKey<Borrow>(b => b.BookId);
            modelBuilder.Entity<Borrow>().HasOne(b => b.Member);
            modelBuilder.Entity<Borrow>().HasAlternateKey(b => b.Guid);
            modelBuilder.Entity<Borrow>().Property(b => b.Guid).ValueGeneratedOnAdd();

            modelBuilder.Entity<Employee>().HasAlternateKey(e => e.Guid);

            modelBuilder.Entity<Member>().HasMany(m => m.Borrows);
            modelBuilder.Entity<Member>().HasAlternateKey(m => m.Guid);
            modelBuilder.Entity<Member>().Property(m => m.Guid).ValueGeneratedOnAdd();
        }

        public void Seed()
        {
            Randomizer.Seed = new Random(69420);

            var author = new Faker<Author>("de").CustomInstantiator(a => new Author(
                 firstname: a.Person.FirstName,
                 lastname: a.Person.LastName,
                 birthDate: a.Person.DateOfBirth,
                 nationality: a.Address.Country()
                ))
                .Generate(30)
                .ToList();
            Authors.AddRange(author);
            SaveChanges();

            var book = new Faker<Book>("de").CustomInstantiator(b => new Book(
                title: b.Commerce.ProductName(),
                author: b.Random.ListItem(author),
                publisher: b.Company.CompanyName(),
                year: b.Date.Past().Year,
                rating: b.Random.Int(0, 10)
                ))
                .Generate(100)
                .ToList();

            Books.AddRange(book);
            SaveChanges();

            var member = new Faker<Member>("de").CustomInstantiator(m => new Member(
                firstname: m.Person.FirstName,
                lastname: m.Person.LastName,
                isAdmin: m.Random.Bool()
                ))
                .Generate(50)
                .ToList();

            Members.AddRange(member);
            SaveChanges();

            var role = new List<Role>() { new Role("Manager"), new Role("Bibliothekar"), new Role("Assistent") };
            Roles.AddRange(role);
            SaveChanges();

            var employee = new Faker<Employee>("de").CustomInstantiator(e => new Employee(
                firstname: e.Person.FirstName,
                lastname: e.Person.LastName,
                role: e.Random.ListItem(role)
                ))
                .Generate(20)
                .ToList();

            Employees.AddRange(employee);
            SaveChanges();

            var borrow = new Faker<Borrow>("de").CustomInstantiator(b => new Borrow(
                book: b.Random.ListItem(book),
                member: b.Random.ListItem(member)
                ))
                .Generate(40)
                .ToList();

            Borrows.AddRange(borrow);
            SaveChanges();
        }
    }
}
