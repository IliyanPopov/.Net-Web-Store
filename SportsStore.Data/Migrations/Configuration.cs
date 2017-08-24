namespace SportsStore.Data.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Models.Entities;

    internal sealed class Configuration : DbMigrationsConfiguration<SportsStore.Data.SportsShopContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.ContextKey = "SportsStore.Data.SportsShopContext";
        }

        protected override void Seed(SportsStore.Data.SportsShopContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            var products = new List<Product>
            {
                new Product {Name = "Football-Shirt", Price = 25, Description = "Some cool football is here",Categoty = "FootBall"},
                new Product {Name = "Surf board", Price = 179,Description = "Cool swimming",Categoty = "Summer"},
                new Product {Name = "Running shoes", Price = 95,Description = "Sport is healthy", Categoty = "Sport"}
            };

            products.ForEach(p => context.Products.AddOrUpdate(p));
            context.SaveChanges();
        }
    }
}
