namespace SportsStore.WebUI.ViewModels
{
    using System.Collections.Generic;
    using Models;
    using SportsStore.Data.Contracts;
    using SportsStore.Models.Contracts;

    public class ProductsListViewModel
    {
        public IEnumerable<IProduct> Products { get; set; }

        public PagingInfo PagingInfo { get; set; }

        public string CurrentCategory { get; set; }
    }
}