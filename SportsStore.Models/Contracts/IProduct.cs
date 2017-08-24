namespace SportsStore.Models.Contracts
{
    public interface IProduct
    {
        int ProductId { get; }

        string Name { get; }

        string Description { get; }

        decimal Price { get; }

        string Categoty { get; }
    }
}