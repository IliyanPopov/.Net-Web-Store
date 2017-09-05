namespace SportsStore.Models.Entities
{
    using Contracts;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class Product : IProduct
    {
        [HiddenInput(DisplayValue = false)]
        public int ProductId { get; set; }

        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public decimal Price { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }
    }
}