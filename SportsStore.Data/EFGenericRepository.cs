namespace SportsStore.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using Contracts;

    public class EfGenericRepository<T> : IRepository<T> where T : class
    {
        public EfGenericRepository(DbContext context)
        {
            if (context == null)
            {
                throw new ArgumentException("An instance of DbContext is required to use this repository.",
                    nameof(context));
            }

            this.Context = context;
            this.DbSet = this.Context.Set<T>();
        }

        protected DbContext Context { get; set; }

        protected IDbSet<T> DbSet { get; set; }

        public virtual IQueryable<T> All => this.DbSet.AsQueryable();

        public int SaveChanges()
        {
            return this.Context.SaveChanges();
        }

        public void Add(T entity)
        {
            var entry = this.Context.Entry(entity);

            if (entry.State != EntityState.Detached)
            {
                entry.State = EntityState.Added;
            }
            else
            {
                this.DbSet.Add(entity);
            }
        }

        public int Delete(T entity)
        {
            this.DbSet.Remove(entity);
            return this.Context.SaveChanges();
        }


        public virtual void Update(T entity)
        {
            var entry = this.Context.Entry(entity);

            if (entry.State == EntityState.Detached)
            {
                this.DbSet.Attach(entity);
            }

            entry.State = EntityState.Modified;
        }
    }
}