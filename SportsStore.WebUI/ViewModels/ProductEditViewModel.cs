namespace SportsStore.WebUI.ViewModels
{
    using System.Collections.Generic;
    using SportsStore.Models.Entities;

    public class ProductEditViewModel
    {
        public int CategoryId { get; set; }

        public int ProductId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }


        public ICollection<Category> Categories { get; set; }
    }
}