namespace SportsStore.Data.Contracts
{
    using System.Collections.Generic;
    using Models.Contracts;

    public interface IGenericRepository<T>
    {
        IEnumerable<T> All { get; }

        //T GetById(object id);

        //void Add(T entity);

        //void Update(T entity);

        //int SaveChanges();
    }
}
