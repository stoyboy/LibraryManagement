using LibraryManagement.Application.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Infrastructure.Repositories
{
    public class RoleRepository : Repository<Role, int>
    {
        public RoleRepository(LibraryContext db) : base(db)
        {
        }
    }
}
