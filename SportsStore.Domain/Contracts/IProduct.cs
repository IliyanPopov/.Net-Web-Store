namespace SportsStore.Domain.Contracts
{
    public interface IProduct
    {
        int ProductId { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        decimal Price { get; set; }
        string Categoty { get; set; }
    }
}