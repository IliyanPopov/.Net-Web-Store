namespace SportsStore.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Data.Contracts;
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
        [SetUp]
        public void CreateListWithProductsAndCategories()
        {
            Category cat1 = new Category {Name = "Cat1"};
            Category cat2 = new Category {Name = "Cat2"};
            Category cat3 = new Category {Name = "Cat3"};

            this._products = new List<Product>
            {
                new Product {ProductId = 1, Name = "P1", Category = cat1},
                new Product {ProductId = 2, Name = "P2", Category = cat2},
                new Product {ProductId = 3, Name = "P3", Category = cat1},
                new Product {ProductId = 4, Name = "P4", Category = cat2},
                new Product {ProductId = 5, Name = "P5", Category = cat3}
            };
        }

        private IList<Product> _products;

        [Test]
        public void Can_Filter_Products()
        {
            // Arrange
            Category cat1 = new Category {Name = "Cat1"};
            Category cat2 = new Category {Name = "Cat2"};
            Category cat3 = new Category {Name = "Cat3"};
            // mock the repository
            Mock<IRepository<Product>> mock = new Mock<IRepository<Product>>();
            mock.Setup(m => m.All).Returns(this._products.AsQueryable);

            // create a controller and make the page size 3 items

            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 3;

            // Action     
            Product[] result = ((ProductsListViewModel) controller.List(cat2.Name, 1).Model)
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
        public void PageCountForSpecificCategoriesAreCorrect()
        {
            // Arrange
            // - create the mock repository
            Mock<IRepository<Product>> mock = new Mock<IRepository<Product>>();
            mock.Setup(m => m.All).Returns(this._products.AsQueryable);

            // Arrange - create a controller and make the page size 3 items
            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 3;

            // Action - test the product counts for different categories
            int res1 = ((ProductsListViewModel) controller.List("Cat1").Model).PagingInfo.TotalItems;
            int res2 = ((ProductsListViewModel) controller.List("Cat2").Model).PagingInfo.TotalItems;
            int res3 = ((ProductsListViewModel) controller.List("Cat3").Model).PagingInfo.TotalItems;
            int resAll = ((ProductsListViewModel) controller.List(null).Model).PagingInfo.TotalItems;

            // Assert
            Assert.AreEqual(res1, 2);
            Assert.AreEqual(res2, 2);
            Assert.AreEqual(res3, 1);
            Assert.AreEqual(resAll, 5);
        }

        [Test]
        public void PaginationIsCorrectlySentToTheViewModel()
        {
            // Arrange
            Mock<IRepository<Product>> mock = new Mock<IRepository<Product>>();
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
            ProductsListViewModel result = (ProductsListViewModel) controller.List(null, 2).Model;

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
            Mock<IRepository<Product>> mock = new Mock<IRepository<Product>>();
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
            ProductsListViewModel result = (ProductsListViewModel) controller.List(null, 2).Model;

            // Assert
            Product[] prodArray = result.Products.ToArray();
            Assert.IsTrue(prodArray.Length == 2);
            Assert.AreEqual(prodArray[0].Name, "P4");
            Assert.AreEqual(prodArray[1].Name, "P5");
        }
    }
}