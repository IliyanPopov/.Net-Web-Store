namespace SportsStore.Domain.Contracts
{
    using System.Collections.Generic;

    public interface IProductRepository
    {
        IEnumerable<IProduct> Products { get; }
    }
}
