using Laboratoria.Entities;
using System.Data.Entity;

namespace Laboratoria.Contexts
{
    /// <summary>Контекст EF для БД.</summary>
    class UsersSmenaContext : DbContext
    {
        public UsersSmenaContext()
            : base("DefaultConnection")
        {

        }
        /// <summary>Таблица всех работников всех смен.</summary>
        public DbSet<UserSmenaEntity> UsersSmenas { get; set; }
    }
}
