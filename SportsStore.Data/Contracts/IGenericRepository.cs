namespace SportsStore.Data.Contracts
{
    using System.Collections.Generic;
    using System.Linq;
    using Data.Contracts;

    public interface IGenericRepository<T>
    {
        IQueryable<T> All { get; }

        //T GetById(object id);

        //void Add(T entity);

        //void Update(T entity);

        //int SaveChanges();
    }
}
