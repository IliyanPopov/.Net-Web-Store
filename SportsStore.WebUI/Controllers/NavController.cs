namespace SportsStore.WebUI.Controllers
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Castle.Core.Logging;
    using Data.Contracts;
    using Ninject.Infrastructure.Language;
    using SportsStore.Models.Entities;
    using ViewModels;

    public class NavController : Controller
    {
        private readonly IGenericRepository<Category> _categoryRepository;

        public NavController(IGenericRepository<Category> categoryRepository)
        {
            this._categoryRepository = categoryRepository;
        }

        public PartialViewResult Menu(string category = null)
        {
            CategoriesListViewModel model = new CategoriesListViewModel
            {
                Categories = this._categoryRepository.All.Select(p => p.Name)
                    .Distinct()
                    .OrderBy(c => c).ToEnumerable(),
                CurrentCategory = category
            };

            return PartialView(model);
        }
    }
}
// TODO refactor categories to be different entity with relation to product