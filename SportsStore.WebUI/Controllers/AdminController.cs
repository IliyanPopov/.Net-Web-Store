namespace SportsStore.WebUI.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Data.Contracts;
    using SportsStore.Models.Entities;

    public class AdminController : Controller
    {
        private readonly IGenericRepository<Product> _productsRepository;

        public AdminController(IGenericRepository<Product> productsRepository)
        {
            this._productsRepository = productsRepository;
        }

        public ViewResult Index()
        {
            return View(this._productsRepository.All);
        }

        public ViewResult Edit(int productId)
        {
            Product product = this._productsRepository.All
                .FirstOrDefault(p => p.ProductId == productId);
            return View(product);
        }
    }
}