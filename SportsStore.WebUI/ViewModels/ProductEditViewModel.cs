namespace SportsStore.WebUI.ViewModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using SportsStore.Models.Entities;

    public class ProductEditViewModel
    {
        [Required(ErrorMessage = "Please specify a category")]
        public int CategoryId { get; set; }

        public int ProductId { get; set; }

        [Required(ErrorMessage = "Please enter a product name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter a description")]
        public string Description { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a positive price")]
        public decimal Price { get; set; }

        public ICollection<Category> Categories { get; set; }

    }
}