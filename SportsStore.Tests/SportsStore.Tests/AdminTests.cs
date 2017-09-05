namespace SportsStore.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using Data.Contracts;
    using Models.Entities;
    using Moq;
    using NUnit.Framework;
    using WebUI.Controllers;

    [TestFixture]
    public class AdminTests
    {
        [Test]
        public void Index_Contains_All_Products()
        {
            // Arrange - create the mock repository
            Mock<IGenericRepository<Product>> mock = new Mock<IGenericRepository<Product>>();
            mock.Setup(m => m.All).Returns(new[]
            {
                new Product {ProductId = 1, Name = "P1"},
                new Product {ProductId = 2, Name = "P2"},
                new Product {ProductId = 3, Name = "P3"}
            }.AsQueryable());

            // Arrange - create a controller
            AdminController target = new AdminController(mock.Object);

            // Action
            Product[] result = ((IEnumerable<Product>)target.Index().ViewData.Model).ToArray();

            // Assert
            Assert.AreEqual(result.Length, 3);
            Assert.AreEqual("P1", result[0].Name);
            Assert.AreEqual("P2", result[1].Name);
            Assert.AreEqual("P3", result[2].Name);
        }

        [Test]
        public void ExistringProductsIsEditable()
        {
            // Arrange - create the mock repository
            Mock<IGenericRepository<Product>> mock = new Mock<IGenericRepository<Product>>();
            mock.Setup(m => m.All).Returns(new[]
            {
                new Product {ProductId = 1, Name = "P1"},
                new Product {ProductId = 2, Name = "P2"},
                new Product {ProductId = 3, Name = "P3"}
            }.AsQueryable());


            AdminController target = new AdminController(mock.Object);

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
        public void NonExistringProductsIsNotEditable()
        {
            // Arrange - create the mock repository
            Mock<IGenericRepository<Product>> mock = new Mock<IGenericRepository<Product>>();
            mock.Setup(m => m.All).Returns(new[]
            {
                new Product {ProductId = 1, Name = "P1"},
                new Product {ProductId = 2, Name = "P2"},
                new Product {ProductId = 3, Name = "P3"}
            }.AsQueryable());

            AdminController target = new AdminController(mock.Object);
            // Act
            Product result = (Product)target.Edit(4).ViewData.Model;
            // Assert
            Assert.IsNull(result);
        }
    }

}