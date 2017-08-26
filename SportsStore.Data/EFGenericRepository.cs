namespace SportsStore.Data
{
    using System;
    using System.Collections.Generic;
    using Contracts;

    public class EfProductGenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly SportsShopContext _context;

        public EfProductGenericRepository(SportsShopContext context)
        {
            if (context == null)
            {
                throw new ArgumentException("An instance of DbContext is required to use this repository.",
                    nameof(context));
            }

            this._context = context;
        }


        public virtual IEnumerable<T> All => this._context.Set<T>();
    }
}