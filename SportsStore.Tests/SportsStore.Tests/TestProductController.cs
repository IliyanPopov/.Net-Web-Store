namespace SportsStore.Tests
{
    using System;
    using System.Collections.Generic;
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
            controller.PageSize = 3;
            // Act
            // list with int parameter referes to the page number 
            ProductsListViewModel result = (ProductsListViewModel)controller.List(2).Model;

            // Assert
            IProduct[] prodArray = result.Products.ToArray();
            Assert.IsTrue(prodArray.Length == 2);
            Assert.AreEqual(prodArray[0].Name, "P4");
            Assert.AreEqual(prodArray[1].Name, "P5");
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
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductId = 1, Name = "P1"},
                new Product {ProductId = 2, Name = "P2"},
                new Product {ProductId = 3, Name = "P3"},
                new Product {ProductId = 4, Name = "P4"},
                new Product {ProductId = 5, Name = "P5"},
            });

            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 3;
            // Act
            ProductsListViewModel result = (ProductsListViewModel)controller.List(2).Model;

            // Assert
            PagingInfo pageInfo = result.PagingInfo;
            Assert.AreEqual(pageInfo.CurrentPage, 2);
            Assert.AreEqual(pageInfo.ItemsPerPage, 3);
            Assert.AreEqual(pageInfo.TotalItems, 5);
            Assert.AreEqual(pageInfo.TotalPages, 2);
        }
    }
}