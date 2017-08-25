namespace SportsStore.Models.Entities
{
    using Contracts;

    public class Product : IProduct
    {
        public int ProductId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string Category { get; set; }
    }
}