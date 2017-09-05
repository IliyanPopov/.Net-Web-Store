namespace SportsStore.Models.Entities
{
    using System.Collections.Generic;

    public class Category 
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