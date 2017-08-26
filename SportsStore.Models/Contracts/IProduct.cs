namespace SportsStore.Models.Contracts
{
    using Entities;

    public interface IProduct
    {
        int ProductId { get; }

        string Name { get; }

        string Description { get; }

        decimal Price { get; }

        Category Category { get; }
    }
}