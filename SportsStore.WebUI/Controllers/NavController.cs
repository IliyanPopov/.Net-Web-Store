namespace SportsStore.WebUI.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Data.Contracts;
    using SportsStore.Models.Entities;

    public class NavController : Controller
    {
        private readonly IGenericRepository<Category> _categoryRepository;

        public NavController(IGenericRepository<Category> categoryRepository)
        {
            this._categoryRepository = categoryRepository;
        }

        public PartialViewResult Menu()
        {
            IEnumerable<string> categories = this._categoryRepository.All.Select(p => p.Name)
                .Distinct()
                .OrderBy(c => c);

            return PartialView(categories);
        }
    }
}
// TODO refactor categories to be different entity with relation to product