namespace SportsStore.Data.Contracts
{
    using System.Collections.Generic;
    using Models.Contracts;

    public interface IProductRepository
    {
        IEnumerable<IProduct> Products { get; }
    }
}
