namespace SportsStore.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using Data.Contracts;
    using Models.Contracts;
    using Models.Entities;
    using Moq;
    using NUnit.Framework;
    using WebUI.Controllers;

    [TestFixture]
    public class TestProductController
    {
        [Test]
        public void PaginationShowCorrectNumberOfProductsPerPage()
        {
            // Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new IProduct[]
            {
                new Product {ProductId = 1, Name = "P1"},
                new Product {ProductId = 2, Name = "P2"},
                new Product {ProductId = 3, Name = "P3"},
                new Product {ProductId = 4, Name = "P4"},
                new Product {ProductId = 5, Name = "P5"},
            });
            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 2;
            // Act
            // list with int parameter referes to the page number 
            IEnumerable<IProduct> result = (IEnumerable<IProduct>)controller.List(3).Model;

            // Assert
            IProduct[] prodArray = result.ToArray();
            Assert.IsTrue(prodArray.Length == 1);
            Assert.AreEqual(prodArray[0].Name, "P5");
        }
    }
}