namespace SportsStore.Models.Entities
{
    using System.Collections.Generic;
    using Contracts;

    public class Category : ICategory
    {
        public Category()
        {
            this.Products = new HashSet<Product>();
        }

        public virtual ICollection<Product> Products { get; set; }

        public int CategoryId { get; set; }

        public string Name { get; set; }
    }
}