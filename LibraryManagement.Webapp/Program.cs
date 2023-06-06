using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using LibraryManagement.Application.Infrastructure;
using LibraryManagement.Webapp.Dto;
using LibraryManagement.Application.Infrastructure.Repositories;

// Erstellen und seeden der Datenbank
var opt = new DbContextOptionsBuilder()
    .UseSqlite("Data Source=stores.db")  // Keep connection open (only needed with SQLite in memory db)
    .Options;
using (var db = new LibraryContext(opt))
{
    db.Database.EnsureDeleted();
    db.Database.EnsureCreated();
    db.Seed();
}

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddDbContext<LibraryContext>(opt =>
{
    opt.UseSqlite("Data Source=stores.db");
});
builder.Services.AddTransient<AuthorRepository>();
builder.Services.AddTransient<BookRepository>();
builder.Services.AddTransient<EmployeeRepository>();
builder.Services.AddTransient<RoleRepository>();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddRazorPages();

// MIDDLEWARE
var app = builder.Build();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.MapRazorPages();
app.Run();