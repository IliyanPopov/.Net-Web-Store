namespace SportsStore.Tests
{
    using System.Linq;
    using System.Web.Mvc;
    using Data.Contracts;
    using Models.Entities;
    using Moq;
    using NUnit.Framework;
    using WebUI.Controllers;

    [TestFixture]
    public class ImageTests
    {
        [Test]
        public void Can_Retrieve_Image_Data()
        {
            // Arrange - create a Product with image data
            Product product = new Product
            {
                ProductId = 2,
                Name = "Test",
                ImageData = new byte[] { },
                ImageMimeType = "image/png"
            };
            // Arrange - create the mock repository
            Mock<IRepository<Product>> mock = new Mock<IRepository<Product>>();
            mock.Setup(m => m.All).Returns(new[]
            {
                new Product {ProductId = 1, Name = "P1"},
                product,
                new Product {ProductId = 3, Name = "P3"}
            }.AsQueryable);

            // Arrange - create the controller
            ProductController target = new ProductController(mock.Object);
            // Act - call the GetImage action method
            ActionResult result = target.GetImage(2);
            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(FileResult), result);
            Assert.AreEqual(product.ImageMimeType,
                ((FileResult)result).ContentType);
        }

        [Test]
        public void Cannot_Retrieve_Image_Data_For_Invalid_ID()
        {
            // Arrange - create the mock repository
            Mock<IRepository<Product>> mock = new Mock<IRepository<Product>>();
            mock.Setup(m => m.All).Returns(new[]
            {
                new Product {ProductId = 1, Name = "P1"},
                new Product {ProductId = 3, Name = "p2"}
            }.AsQueryable);

            // Arrange - create the controller
            ProductController target = new ProductController(mock.Object);

            // Act - call the GetImage action method
            ActionResult result = target.GetImage(100);

            // Assert
            Assert.IsNull(result);
        }
    }
}