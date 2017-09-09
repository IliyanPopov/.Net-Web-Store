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

            Mock<IRepository<Category>> mock = new Mock<IRepository<Category>>();

            mock.Setup(m => m.All).Returns(new[]
            {
                new Category {Name = "Apples"},
                new Category {Name = "Plums"},
                new Category {Name = "Oranges"}
            }.AsQueryable);

            NavController controller = new NavController(mock.Object);

            // Act
            CategoriesListViewModel result = (CategoriesListViewModel)controller.Menu().Model;
            // Assert
            Assert.AreEqual(result.Categories.Count(), 3);
            Assert.IsTrue(result.Categories.Any(c =>c == "Apples"));
            Assert.IsTrue(result.Categories.Any(c => c == "Plums"));
            Assert.IsTrue(result.Categories.Any(c => c == "Oranges"));
        }

        [Test]
        public void CurrentCategoryIsColoredDifferently()
        {
            // Arrange
            // - create the mock repository
            Mock<IRepository<Category>> mock = new Mock<IRepository<Category>>();

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