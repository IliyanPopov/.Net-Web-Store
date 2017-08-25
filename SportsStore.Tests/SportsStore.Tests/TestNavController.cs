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
    internal class TestNavController
    {

        [Test]
        public void CategoriesAreCreated()
        {
            // Arrange

            Mock<IProductRepository> mock = new Mock<IProductRepository>();

            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductId = 1, Name = "P1", Category = "Apples"},
                new Product {ProductId = 2, Name = "P2", Category = "Apples"},
                new Product {ProductId = 3, Name = "P3", Category = "Plums"},
                new Product {ProductId = 4, Name = "P4", Category = "Oranges"},
            });

            NavController controller = new NavController(mock.Object);

            // Act
            string[] result = ((IEnumerable<string>)controller.Menu().Model).ToArray();

            // Assert
            Assert.AreEqual(result.Length, 3);
            Assert.AreEqual(result[0], "Apples");
            Assert.AreEqual(result[1], "Oranges");
            Assert.AreEqual(result[2], "Plums");
        }
    }
}