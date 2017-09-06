namespace SportsStore.WebUI.Controllers
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Web.Mvc;
    using Data.Contracts;
    using SportsStore.Models.Entities;
    using ViewModels;

    public class AdminController : Controller
    {
        private readonly IRepository<Category> _categoriesRepository;
        private readonly IRepository<Product> _productsRepository;


        public AdminController(IRepository<Product> productsRepository, IRepository<Category> categories)
        {
            this._productsRepository = productsRepository;
            this._categoriesRepository = categories;
        }

        public ViewResult Index()
        {
            return View(this._productsRepository.All);
        }

        public ViewResult Edit(int productId)
        {
            Product product = this._productsRepository.All
                .Include(p => p.Category)
                .FirstOrDefault(p => p.ProductId == productId);

            if (product == null)
            {
                throw new ArgumentNullException("Product is not existring!");
            }

            ProductEditViewModel viewmodel = new ProductEditViewModel
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                CategoryId = product.CategoryId,
                Categories = this._categoriesRepository.All.OrderBy(c => c.Name).ToList()
            };

            return View(viewmodel);
        }

        [HttpPost]
        public ActionResult Edit(int productId, ProductEditViewModel viewModel)
        {
            if (!this.ModelState.IsValid)
            {
                // there is something wrong with the data values
                return View(viewModel);
            }
            var product = this._productsRepository.All.FirstOrDefault(p => p.ProductId == productId);

            if (product == null)
            {
                return HttpNotFound();
            }

            product.Name = viewModel.Name;
            product.Description = viewModel.Description;
            product.Price = viewModel.Price;
            product.CategoryId = viewModel.CategoryId;

            this._productsRepository.SaveChanges();
            this.TempData["message"] = $"{product.Name} has been saved";
            return RedirectToAction("Index");
        }
    }
}