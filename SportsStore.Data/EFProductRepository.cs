namespace SportsStore.Data
{
    using System.Collections.Generic;
    using Contracts;
    using Models.Contracts;

    public class EFProductRepository : IProductRepository
    {
        private readonly SportsShopContext _context = new SportsShopContext();

        public IEnumerable<IProduct> Products
        {
            get { return this._context.Products; }
        }
    }
}