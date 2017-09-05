namespace SportsStore.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Data.Contracts;
    using Models.Entities;
    using Moq;
    using NUnit.Framework;
    using WebUI.Controllers;

    [TestFixture]
    public class AdminTests
    {
        [Test]
        public void Can_Save_Valid_Changes()
        {
            // Arrange - create mock repository
            Mock<IRepository<Product>> mockProducts = new Mock<IRepository<Product>>();
            Mock<IRepository<Category>> mockCategories = new Mock<IRepository<Category>>();
            AdminController target = new AdminController(mockProducts.Object, mockCategories.Object);
            Product product = new Product { Name = "Test", ProductId = 1 };

            // Act - try to save the product
            ActionResult result = target.Edit(product.ProductId);

            // Assert - check that the repository was called
            mockProducts.Verify(m => m.SaveChanges());
            Assert.IsNotInstanceOf(typeof(ViewResult), result);
        }

        [Test]
        public void Cannot_Save_Invalid_Changes()
        {
            // Arrange - create mock repository
            Mock<IRepository<Product>> mockProducts = new Mock<IRepository<Product>>();
            Mock<IRepository<Category>> mockCategories = new Mock<IRepository<Category>>();


            mockCategories.Setup(m => m.All).Returns(new[]
            {
                new Category { CategoryId = 1,Name = "Winter"},
                new Category { CategoryId = 2,Name = "Summer"},
                new Category { CategoryId = 3,Name = "Sprint"}
            }.AsQueryable);

            mockProducts.Setup(m => m.All).Returns(new[]
            {
                new Product { Name = "Test1", ProductId = 1, Description = "TestDescription1", Price = 101, Category = mockCategories.Object.All.FirstOrDefault(c => c.CategoryId == 1), CategoryId = 1 },
                new Product { Name = "Test2", ProductId = 2, Description = "TestDescription2", Price = 102, Category = mockCategories.Object.All.FirstOrDefault(c => c.CategoryId == 2), CategoryId = 2 },
                new Product { Name = "Test3", ProductId = 3, Description = "TestDescription3", Price = 103, Category = mockCategories.Object.All.FirstOrDefault(c => c.CategoryId == 3), CategoryId = 3 }
            }.AsQueryable());
            AdminController target = new AdminController(mockProducts.Object, mockCategories.Object);

            Product product = mockProducts.Object.All.FirstOrDefault(p => p.ProductId == 1);
            target.ModelState.AddModelError("error", "error");

            // Act - try to save the product
            ActionResult result = target.Edit(product.ProductId);

            // Assert - check that the repository was not called
            mockProducts.Verify(m => m.SaveChanges(), Times.Never());
            // Assert - check the method result type
            Assert.IsInstanceOf(typeof(ViewResult), result);

            //TODO this way is way to hard, need to have a look at automapper, so i can map the viewmodels automaticly
        }

        [Test]
        public void ExistringProductsIsEditable()
        {
            // Arrange - create the mock repository
            Mock<IRepository<Product>> mockProducts = new Mock<IRepository<Product>>();
            Mock<IRepository<Category>> mockCategories = new Mock<IRepository<Category>>();

            mockProducts.Setup(m => m.All).Returns(new[]
            {
                new Product {ProductId = 1, Name = "P1"},
                new Product {ProductId = 2, Name = "P2"},
                new Product {ProductId = 3, Name = "P3"}
            }.AsQueryable());


            AdminController target = new AdminController(mockProducts.Object, mockCategories.Object);

            // Act
            Product p1 = target.Edit(1).ViewData.Model as Product;
            Product p2 = target.Edit(2).ViewData.Model as Product;
            Product p3 = target.Edit(3).ViewData.Model as Product;

            // Assert
            Assert.AreEqual(1, p1.ProductId);
            Assert.AreEqual(2, p2.ProductId);
            Assert.AreEqual(3, p3.ProductId);
        }

        [Test]
        public void Index_Contains_All_Products()
        {
            // Arrange - create the mock repository
            Mock<IRepository<Product>> mockProducts = new Mock<IRepository<Product>>();
            Mock<IRepository<Category>> mockCategories = new Mock<IRepository<Category>>();

            mockProducts.Setup(m => m.All).Returns(new[]
            {
                new Product {ProductId = 1, Name = "P1"},
                new Product {ProductId = 2, Name = "P2"},
                new Product {ProductId = 3, Name = "P3"}
            }.AsQueryable());

            // Arrange - create a controller
            AdminController target = new AdminController(mockProducts.Object, mockCategories.Object);

            // Action
            Product[] result = ((IEnumerable<Product>)target.Index().ViewData.Model).ToArray();

            // Assert
            Assert.AreEqual(result.Length, 3);
            Assert.AreEqual("P1", result[0].Name);
            Assert.AreEqual("P2", result[1].Name);
            Assert.AreEqual("P3", result[2].Name);
        }

        [Test]
        public void NonExistringProductsIsNotEditable()
        {
            // Arrange - create the mock repository
            Mock<IRepository<Product>> mockProducts = new Mock<IRepository<Product>>();
            Mock<IRepository<Category>> mockCategories = new Mock<IRepository<Category>>();

            mockProducts.Setup(m => m.All).Returns(new[]
            {
                new Product {ProductId = 1, Name = "P1"},
                new Product {ProductId = 2, Name = "P2"},
                new Product {ProductId = 3, Name = "P3"}
            }.AsQueryable());

            AdminController target = new AdminController(mockProducts.Object, mockCategories.Object);
            // Act
            Product result = (Product)target.Edit(4).ViewData.Model;
            // Assert
            Assert.IsNull(result);
        }
    }
}