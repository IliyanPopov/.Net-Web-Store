namespace SportsStore.Data.Migrations
{
    using System.Data.Entity.Migrations;
    using Models.Entities;

    internal sealed class Configuration : DbMigrationsConfiguration<SportsShopContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
            this.ContextKey = "SportsStore.Data.SportsShopContext";
        }

        protected override void Seed(SportsShopContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            context.Products.AddOrUpdate(p => p.Name,
                new Product
                {
                    Name = "Football-Shirt",
                    Price = 25,
                    Description = "Some cool football is here",
                    Category = "FootBall"
                },
                new Product {Name = "Surf board", Price = 179, Description = "Cool swimming", Category = "Summer"},
                new Product {Name = "Running shoes", Price = 95, Description = "Sport is healthy", Category = "Sport"},
                new Product {Name = "BasketBall", Price = 25, Description = "Sport is for jumpers", Category = "Sport"},
                new Product {Name = "Raft", Price = 1195, Description = "at sea is best ", Category = "Summer"},
                new Product {Name = "Hikes", Price = 395, Description = "snow is not bad too", Category = "Winter"});

            context.SaveChanges();
        }
    }
}