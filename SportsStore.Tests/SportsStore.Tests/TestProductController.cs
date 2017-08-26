namespace SportsStore.Tests
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using Data.Contracts;
    using Models.Contracts;
    using Models.Entities;
    using Moq;
    using NUnit.Framework;
    using WebUI.Controllers;
    using WebUI.HtmlHelpers;
    using WebUI.Models;
    using WebUI.ViewModels;

    [TestFixture]
    public class TestProductController
    {
        [Test]
        public void Can_Filter_Products()
        {
            // Arrange
            Category cat1 = new Category { Name = "Cat1" };
            Category cat2 = new Category { Name = "Cat2" };
            Category cat3 = new Category { Name = "Cat3" };
            // mock the repository
            Mock<IGenericRepository<Product>> mock = new Mock<IGenericRepository<Product>>();
            mock.Setup(m => m.All).Returns(new[]
            {
                new Product {ProductId = 1, Name = "P1", Category = cat1},
                new Product {ProductId = 2, Name = "P2", Category = cat2},
                new Product {ProductId = 3, Name = "P3", Category = cat1},
                new Product {ProductId = 4, Name = "P4", Category = cat2},
                new Product {ProductId = 5, Name = "P5", Category = cat3}
            }.AsQueryable);

            // create a controller and make the page size 3 items

            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 3;

            // Action     
            IProduct[] result = ((ProductsListViewModel)controller.List(cat2.Name, 1).Model)
                .Products.ToArray();

            // Assert
            Assert.AreEqual(result.Length, 2);
            Assert.IsTrue(result[0].Name == "P2" && result[0].Category.Name == "Cat2");
            Assert.IsTrue(result[1].Name == "P4" && result[1].Category.Name == "Cat2");
        }

        [Test]
        public void HtmlHelperPagingLinksAreGeneratedCorrectly()
        {
            // Arrange
            // needed to apply the extention method
            HtmlHelper helper = null;
            PagingInfo pagingInfo = new PagingInfo
            {
                CurrentPage = 2,
                TotalItems = 28,
                ItemsPerPage = 10
            };

            Func<int, string> pageUrlDelegae = i => "Page" + i;

            // Act
            MvcHtmlString result = helper.PageLinks(pagingInfo, pageUrlDelegae);

            // Assert
            Assert.AreEqual(@"<a class=""btn btn-default"" href=""Page1"">1</a>"
                            + @"<a class=""btn btn-default btn-primary selected"" href=""Page2"">2</a>"
                            + @"<a class=""btn btn-default"" href=""Page3"">3</a>",
                result.ToString());
        }

        [Test]
        public void PaginationIsCorrectlySentToTheViewModel()
        {
            // Arrange
            Mock<IGenericRepository<Product>> mock = new Mock<IGenericRepository<Product>>();
            mock.Setup(m => m.All).Returns(new[]
            {
                new Product {ProductId = 1, Name = "P1"},
                new Product {ProductId = 2, Name = "P2"},
                new Product {ProductId = 3, Name = "P3"},
                new Product {ProductId = 4, Name = "P4"},
                new Product {ProductId = 5, Name = "P5"}
            }.AsQueryable);

            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 3;
            // Act
            ProductsListViewModel result = (ProductsListViewModel)controller.List(null, 2).Model;

            // Assert
            PagingInfo pageInfo = result.PagingInfo;
            Assert.AreEqual(pageInfo.CurrentPage, 2);
            Assert.AreEqual(pageInfo.ItemsPerPage, 3);
            Assert.AreEqual(pageInfo.TotalItems, 5);
            Assert.AreEqual(pageInfo.TotalPages, 2);
        }

        [Test]
        public void PaginationShowCorrectNumberOfProductsPerPage()
        {
            // Arrange
            Mock<IGenericRepository<Product>> mock = new Mock<IGenericRepository<Product>>();
            mock.Setup(m => m.All).Returns(new[]
            {
                new Product {ProductId = 1, Name = "P1"},
                new Product {ProductId = 2, Name = "P2"},
                new Product {ProductId = 3, Name = "P3"},
                new Product {ProductId = 4, Name = "P4"},
                new Product {ProductId = 5, Name = "P5"}
            }.AsQueryable);
            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 3;
            // Act
            // list with int parameter referes to the page number 
            ProductsListViewModel result = (ProductsListViewModel)controller.List(null, 2).Model;

            // Assert
            IProduct[] prodArray = result.Products.ToArray();
            Assert.IsTrue(prodArray.Length == 2);
            Assert.AreEqual(prodArray[0].Name, "P4");
            Assert.AreEqual(prodArray[1].Name, "P5");
        }
    }
}