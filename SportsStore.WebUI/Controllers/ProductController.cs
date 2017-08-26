namespace SportsStore.WebUI.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Data.Contracts;
    using Models;
    using SportsStore.Models.Contracts;
    using SportsStore.Models.Entities;
    using ViewModels;

    public class ProductController : Controller
    {
        private readonly IGenericRepository<Product> _productGenericRepository;
        public int PageSize = 2;

        public ProductController(IGenericRepository<Product> productProductGenericRepository)
        {
            this._productGenericRepository = productProductGenericRepository;
        }

        public ViewResult List(string category, int page = 1)
        {
            ProductsListViewModel model = new ProductsListViewModel
            {
                Products = this._productGenericRepository.All
                .Where(p => category == null || p.Category == category)
                    .OrderBy(p => p.ProductId)
                    .Skip((page - 1) * this.PageSize)
                    .Take(this.PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = this.PageSize,
                    TotalItems = this._productGenericRepository.All.Count()
                },
                CurrentCategory = category
            };
            return View(model);
        }
    }
}