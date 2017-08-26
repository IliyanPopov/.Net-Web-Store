namespace SportsStore.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using Data.Contracts;
    using Models.Entities;
    using Moq;
    using NUnit.Framework;
    using WebUI.Controllers;
    using WebUI.ViewModels;

    [TestFixture]
    internal class TestNavController
    {
        [Test]
        public void CategoriesAreCreated()
        {
            // Arrange

            Mock<IGenericRepository<Category>> mock = new Mock<IGenericRepository<Category>>();

            mock.Setup(m => m.All).Returns(new[]
            {
                new Category {Name = "Apples"},
                new Category {Name = "Plums"},
                new Category {Name = "Oranges"}
            }.AsQueryable);

            NavController controller = new NavController(mock.Object);

            // Act
            string[] result = ((IEnumerable<string>)controller.Menu().Model).ToArray();

            // Assert
            Assert.AreEqual(result.Length, 3);
            Assert.AreEqual(result[0], "Apples");
            Assert.AreEqual(result[1], "Oranges");
            Assert.AreEqual(result[2], "Plums");
        }

        [Test]
        public void CurrentCategoryIsColoredDifferently()
        {
            // Arrange
            // - create the mock repository
            Mock<IGenericRepository<Category>> mock = new Mock<IGenericRepository<Category>>();

            mock.Setup(m => m.All).Returns(new Category[] {
                new Category { Name = "Apples" },
                new Category { Name = "Oranges" }
            }.AsQueryable());

            NavController controller = new NavController(mock.Object);

            // define the category to select
            string categoryToSelect = "Apples";

            // Act
            CategoriesListViewModel result = (CategoriesListViewModel)controller.Menu(categoryToSelect).Model;

            // Assert
            Assert.AreEqual(categoryToSelect, result.CurrentCategory);
        }
    }
}