namespace SportsStore.Data.Contracts
{
    using System.Linq;

    public interface IRepository<T>
    {
        IQueryable<T> All { get; }

        int SaveChanges();

        void Add(T entity);

        int Delete(T entity);

        //T GetById(object id);

        void Update(T entity);
    }
}