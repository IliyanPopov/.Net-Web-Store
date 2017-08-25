namespace SportsStore.WebUI.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Data.Contracts;
    using Models;
    using ViewModels;

    public class ProductController : Controller
    {
        private readonly IProductRepository _repository;
        public int PageSize = 2;

        public ProductController(IProductRepository productRepository)
        {
            this._repository = productRepository;
        }

        public ViewResult List(string category, int page = 1)
        {
            ProductsListViewModel model = new ProductsListViewModel
            {
                Products = this._repository.Products
                .Where(p => category == null || p.Category == category)
                    .OrderBy(p => p.ProductId)
                    .Skip((page - 1) * this.PageSize)
                    .Take(this.PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = this.PageSize,
                    TotalItems = this._repository.Products.Count()
                },
                CurrentCategory = category
            };
            return View(model);
        }
    }
}