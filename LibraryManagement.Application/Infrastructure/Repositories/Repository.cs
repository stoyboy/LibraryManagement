using LibraryManagement.Application.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Infrastructure.Repositories
{
    public abstract class Repository<Tentity, Tkey> where Tentity : class, IEntity<Tkey> where Tkey : struct
    {
        protected readonly LibraryContext _db;
        public IQueryable<Tentity> Set => _db.Set<Tentity>();

        protected Repository(LibraryContext db)
        {
            _db = db;
        }

        public virtual Tentity? FindById(Tkey id) => _db.Set<Tentity>().FirstOrDefault(e => e.Id.Equals(id));
        public virtual Tentity? FindByGuid(Guid guid) => _db.Set<Tentity>().FirstOrDefault(e => e.Guid.Equals(guid));

        public virtual (bool success, string? message) Insert(Tentity entity)
        {
            _db.Entry(entity).State = EntityState.Added;
            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                return (false, ex.InnerException?.Message ?? ex.Message);
            }
            return (true, string.Empty);
        }

        public virtual (bool success, string? message) Update(Tentity entity)
        {
            if (entity.Id.Equals(default)) { return (false, "Missing primary key."); }

            _db.Entry(entity).State = EntityState.Modified;
            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                return (false, ex.InnerException?.Message ?? ex.Message);
            }
            return (true, string.Empty);
        }

        public virtual (bool success, string? message) Delete(Tentity entity)
        {
            if (entity.Id.Equals(default)) { return (false, "Missing primary key."); }

            _db.Entry(entity).State = EntityState.Deleted;
            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                return (false, ex.InnerException?.Message ?? ex.Message);
            }
            return (true, string.Empty);
        }
    }
}
