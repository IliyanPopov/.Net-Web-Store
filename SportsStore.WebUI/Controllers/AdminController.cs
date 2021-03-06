﻿namespace SportsStore.WebUI.Controllers
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using AutoMapper;
    using Data.Contracts;
    using SportsStore.Models.Entities;
    using ViewModels;

    [Authorize]
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

            var viewmodel = Mapper.Map<ProductEditViewModel>(product);
            viewmodel.Categories = this._categoriesRepository.All.OrderBy(c => c.Name).ToList();


            return View(viewmodel);
        }

        [HttpPost]
        public ActionResult Edit(ProductEditViewModel viewModel, HttpPostedFileBase image = null)
        {
            if (this.ModelState.IsValid)
            {
                var product = this._productsRepository.All.FirstOrDefault(p => p.ProductId == viewModel.ProductId);

                if (image != null)
                {
                    viewModel.ImageMimeType = image.ContentType;
                    viewModel.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(viewModel.ImageData, 0, image.ContentLength);
               
                    if (product == null)
                    {
                        return HttpNotFound();
                    }

                    product.Name = viewModel.Name;
                    product.Description = viewModel.Description;
                    product.Price = viewModel.Price;
                    product.CategoryId = viewModel.CategoryId;
                    product.ImageMimeType = viewModel.ImageMimeType;
                    product.ImageData = viewModel.ImageData;
                }

                this._productsRepository.Update(product);
                this._productsRepository.SaveChanges();
                this.TempData["message"] = $"{viewModel.Name} has been saved";

                return RedirectToAction("Index");
            }
            else
            {
                // there is something wrong with the data values
                return View(viewModel);
            }
        }
        public ViewResult Create()
        {
            var model = new ProductEditViewModel();
            model.Categories = this._categoriesRepository.All.OrderBy(c => c.Name).ToList();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(ProductEditViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                // there is something wrong with the data values
                return View(model);
            }

            var product = Mapper.Map<Product>(model);
            this._productsRepository.Add(product);
            this._productsRepository.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(int productId)
        {
            Product productToDelete = this._productsRepository.All.FirstOrDefault(p => p.ProductId == productId);
            if (productToDelete != null)
            {
                this._productsRepository.Delete(productToDelete);
                this._productsRepository.SaveChanges();
                TempData["message"] = $"{productToDelete.Name} was deleted";
            }
            return RedirectToAction("Index");
        }
    }
}