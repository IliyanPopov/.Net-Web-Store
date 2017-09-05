namespace SportsStore.WebUI.ViewModels
{
    using System.Collections.Generic;
    using Models;
    using SportsStore.Models.Entities;

    public class ProductsListViewModel
    {
        public IEnumerable<Product> Products { get; set; }

        public PagingInfo PagingInfo { get; set; }

        public string CurrentCategory { get; set; }
    }
}