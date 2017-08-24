namespace SportsStore.Data
{
    using System.Data.Entity;
    using Domain.Contracts;
    using Domain.Entities;
    using Migrations;

    public class SportsShopContext : DbContext
    {
        public virtual IDbSet<Product> Products { get; set; }

        public SportsShopContext()
            : base(
                @"data source=(LocalDb)\MSSQLLocalDB;initial catalog=SportsShop;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SportsShopContext, Configuration>());
        }
    }
}