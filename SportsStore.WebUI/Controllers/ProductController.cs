namespace SportsStore.WebUI.Controllers
{
    using System.Web.Mvc;
    using Data;
    using Domain.Contracts;

    public class ProductController : Controller
    {
        private readonly IProductRepository _repository;
        SportsShopContext dbContext = new SportsShopContext();
        public ProductController(IProductRepository productRepository)
        {
            this._repository = productRepository;
        }

        public ViewResult List()
        {
            // return View(this._repository.Products);
            return View(this.dbContext.Products);
        }
    }
}