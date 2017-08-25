namespace SportsStore.WebUI.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using System.Web.UI;
    using Data.Contracts;
    using Models.Contracts;

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
            // return View(this._repository.Products);
            return View(this._repository.Products
                .OrderBy(p => p.ProductId)
                .Skip((page - 1) * PageSize)
                .Take(PageSize));
        }
    }
}