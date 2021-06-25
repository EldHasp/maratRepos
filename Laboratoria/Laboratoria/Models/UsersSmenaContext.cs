using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratoria.Models
{
    class UsersSmenaContext : DbContext
    {
        public UsersSmenaContext() : base("DefaultConnection")
        {

        }
        public DbSet<UserSmena> UsersSmenas { get; set; }
    }
}
