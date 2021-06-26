namespace Laboratoria.Migrations
{
    using Laboratoria.Contexts;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<UsersSmenaContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Laboratoria.Models.UsersSmenaContext";
        }

        protected override void Seed(UsersSmenaContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
