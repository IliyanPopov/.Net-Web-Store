namespace SportsStore.WebUI.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using System.Web.UI;
    using Data.Contracts;
    using Models;
    using SportsStore.Models.Contracts;
    using ViewModels;

    public class ProductController : Controller
    {
        public int PageSize = 2;

        private readonly IProductRepository _repository;

        public ProductController(IProductRepository productRepository)
        {
            this._repository = productRepository;
        }

        public ViewResult List(int page = 1)
        {
            ProductsListViewModel model = new ProductsListViewModel
            {
                Products = this._repository.Products
                    .OrderBy(p => p.ProductId)
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = this.PageSize,
                    TotalItems = this._repository.Products.Count()
                }
            };
            return View(model);
        }
    }
}