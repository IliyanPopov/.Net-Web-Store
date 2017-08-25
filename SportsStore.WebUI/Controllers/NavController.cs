namespace SportsStore.WebUI.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Data.Contracts;

    public class NavController : Controller
    {
        private readonly IProductRepository _repository;

        public NavController(IProductRepository repository)
        {
            this._repository = repository;
        }

        public PartialViewResult Menu()
        {
            IEnumerable<string> categories = this._repository.Products.Select(p => p.Category)
                .Distinct()
                .OrderBy(c => c);

            return PartialView(categories);
        }
    }
}