namespace SportsStore.Data
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using Domain.Entities;

    public class SchoolInitializer : DropCreateDatabaseIfModelChanges<SportsShopContext>
    {
        protected override void Seed(SportsShopContext context)
        {
            var products = new List<Product>
            {
                new Product {Name = "Football", Price = 25},
                new Product {Name = "Surf board", Price = 179},
                new Product {Name = "Running shoes", Price = 95}
            };

            products.ForEach(p => context.Products.Add(p));
            context.SaveChanges();
        }
    }
}