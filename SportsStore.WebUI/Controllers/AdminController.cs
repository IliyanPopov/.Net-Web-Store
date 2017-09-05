namespace SportsStore.WebUI.Controllers
{
    using System.Web.Mvc;
    using Data.Contracts;
    using SportsStore.Models.Entities;

    public class AdminController : Controller
    {
        private IGenericRepository<Product> _products;

        public AdminController(IGenericRepository<Product> products)
        {
            this._products = products;
        }

        public ViewResult Index()
        {
            return View(this._products.All);
        }
    }
}