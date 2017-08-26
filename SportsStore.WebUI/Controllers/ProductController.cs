namespace SportsStore.WebUI.Controllers
{
    using System.Data.Entity;
    using System.Linq;
    using System.Web.Mvc;
    using Data.Contracts;
    using Models;
    using Ninject.Infrastructure.Language;
    using SportsStore.Models.Contracts;
    using SportsStore.Models.Entities;
    using ViewModels;

    public class ProductController : Controller
    {
        private readonly IGenericRepository<Product> _productGenericRepository;
        public int PageSize = 2;

        public ProductController(IGenericRepository<Product> productProductGenericRepository)
        {
            this._productGenericRepository = productProductGenericRepository;
        }

        public ViewResult List(string category, int page = 1)
        {
            ProductsListViewModel model = new ProductsListViewModel
            {
                Products = this._productGenericRepository.All
                .Include(p => p.Category)
                .Where(p => category == null || p.Category.Name == category)
                    .OrderBy(p => p.ProductId)
                    .Skip((page - 1) * this.PageSize)
                    .Take(this.PageSize)
                    .ToEnumerable(),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = this.PageSize,
                    TotalItems = category == null ? this._productGenericRepository.All.Count() : this._productGenericRepository.All.Count(p =>p.Category.Name == category)
                },
                CurrentCategory = category
            };
            return View(model);
        }
    }
}