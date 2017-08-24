namespace SportsStore.WebUI.Controllers
{
    using System.Web.Mvc;
    using Domain.Contracts;

    public class ProductController : Controller
    {
        private readonly IProductRepository _repository;

        public ProductController(IProductRepository productRepository)
        {
            this._repository = productRepository;
        }

        public ViewResult List()
        {
            // return View(this._repository.Products);
            return View(this._repository.Products);
        }
    }
}