namespace SportsStore.WebUI.Controllers
{
    using System.Data.Entity;
    using System.Linq;
    using System.Web.Mvc;
    using Data.Contracts;
    using Models;
    using Ninject.Infrastructure.Language;
    using SportsStore.Models.Entities;
    using ViewModels;

    public class ProductController : Controller
    {
        private readonly IRepository<Product> _productRepository;
        public int PageSize = 2;

        public ProductController(IRepository<Product> productProductRepository)
        {
            this._productRepository = productProductRepository;
        }

        public ViewResult List(string category, int page = 1)
        {
            ProductsListViewModel model = new ProductsListViewModel
            {
                Products = this._productRepository.All
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
                    TotalItems = category == null
                        ? this._productRepository.All.Count()
                        : this._productRepository.All.Count(p => p.Category.Name == category)
                },
                CurrentCategory = category
            };
            return View(model);
        }

        public FileContentResult GetImage(int productId)
        {
            Product product = this._productRepository.All
                .FirstOrDefault(p => p.ProductId == productId);
            if (product != null)
            {
                return File(product.ImageData, product.ImageMimeType);
            }

            return null;
        }
    }
}