namespace SportsStore.WebUI.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Data.Contracts;
    using SportsStore.Models.Contracts;
    using SportsStore.Models.Entities;

    public class NavController : Controller
    {
        private readonly IGenericRepository<Product> _productGenericRepository;

        public NavController(IGenericRepository<Product> productGenericRepository)
        {
            this._productGenericRepository = productGenericRepository;
        }

        public PartialViewResult Menu()
        {
            IEnumerable<string> categories = this._productGenericRepository.All.Select(p => p.Category)
                .Distinct()
                .OrderBy(c => c);

            return PartialView(categories);
        }
    }
}
// TODO refactor categories to be different entity with relation to product