namespace SportsStore.WebUI.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Data.Contracts;
    using Ninject.Infrastructure.Language;
    using SportsStore.Models.Entities;
    using ViewModels;

    public class NavController : Controller
    {
        private readonly IRepository<Category> _categoryRepository;

        public NavController(IRepository<Category> categoryRepository)
        {
            this._categoryRepository = categoryRepository;
        }

        public PartialViewResult Menu(string category = null)
        {
            CategoriesListViewModel categories = new CategoriesListViewModel
            {
                Categories = this._categoryRepository.All.Select(p => p.Name)
                    .Distinct()
                    .OrderBy(c => c).ToEnumerable(),
                CurrentCategory = category
            };

            return PartialView("FlexMenu", categories);
        }
    }
}