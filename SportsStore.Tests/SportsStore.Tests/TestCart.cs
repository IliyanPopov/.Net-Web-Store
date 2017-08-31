namespace SportsStore.Tests
{
    using System.Linq;
    using System.Web.Mvc;
    using Data.Contracts;
    using Models.Entities;
    using Moq;
    using NUnit.Framework;
    using WebUI.Controllers;
    using WebUI.ViewModels;

    [TestFixture]
    public class TestCart
    {
        [Test]
        public void AddingNewItemsToCart()
        {
            // Arrange - create some test products
            Product p1 = new Product {ProductId = 1, Name = "P1"};
            Product p2 = new Product {ProductId = 2, Name = "P2"};
            Cart target = new Cart();

            // Act
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            CartLine[] results = target.Lines.ToArray();

            // Assert
            Assert.AreEqual(results.Length, 2);
            Assert.AreEqual(results[0].Product, p1);
            Assert.AreEqual(results[1].Product, p2);
        }

        [Test]
        public void AddingProductsToCart()
        {
            // Arrange - create the mock repository
            Mock<IGenericRepository<Product>> mock = new Mock<IGenericRepository<Product>>();
            mock.Setup(m => m.All).Returns(new[]
            {
                new Product {ProductId = 1, Name = "P1", Category = new Category {Name = "Apples"}}
            }.AsQueryable());

            Cart cart = new Cart();

            CartController target = new CartController(mock.Object);

            // Act - add a product to the cart
            target.AddToCart(cart, 1, null);

            // Assert
            Assert.AreEqual(cart.Lines.Count(), 1);
            Assert.AreEqual(cart.Lines.ToArray()[0].Product.ProductId, 1);
        }

        [Test]
        public void AddingProductToCartGoesToCartScreen()
        {
            // Arrange - create the mock repository
            Mock<IGenericRepository<Product>> mock = new Mock<IGenericRepository<Product>>();
            mock.Setup(m => m.All).Returns(new[]
            {
                new Product {ProductId = 1, Name = "P1", Category = new Category {Name = "Apples"}}
            }.AsQueryable());

            Cart cart = new Cart();

            CartController target = new CartController(mock.Object);

            // Act - add a product to the cart
            RedirectToRouteResult result = target.AddToCart(cart, 2, "myUrl");

            // Assert
            Assert.AreEqual(result.RouteValues["action"], "Index");
            Assert.AreEqual(result.RouteValues["returnUrl"], "myUrl");
        }

        [Test]
        public void CanViewCartContents()
        {
            // Arrange - create a Cart
            Cart cart = new Cart();

            CartController target = new CartController(null);

            // Act - call the Index action method
            CartIndexViewModel result
                = (CartIndexViewModel) target.Index(cart, "myUrl").ViewData.Model;

            // Assert
            Assert.AreSame(result.Cart, cart);
            Assert.AreEqual(result.ReturnUrl, "myUrl");
        }

        [Test]
        public void ClearMethodRemovesAllItemsFromCart()
        {
            // Arrange - create some test products
            Product p1 = new Product
            {
                ProductId = 1,
                Name = "P1",
                Price = 100M
            };
            Product p2 = new Product {ProductId = 2, Name = "P2", Price = 50M};

            Cart target = new Cart();

            target.AddItem(p1, 1);
            target.AddItem(p2, 1);

            // Act - reset the cart
            target.Clear();

            // Assert
            Assert.AreEqual(target.Lines.Count(), 0);
        }

        [Test]
        public void ComputeTotalValueReturnsCorrectValue()
        {
            // Arrange - create some test products
            Product p1 = new Product {ProductId = 1, Name = "P1", Price = 100M};
            Product p2 = new Product {ProductId = 2, Name = "P2", Price = 50M};
            Cart target = new Cart();

            // Act
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            target.AddItem(p1, 3);
            decimal result = target.ComputeTotalValue();
            // Assert
            Assert.AreEqual(result, 450M);
        }

        [Test]
        public void ExistingProductsQuantityIsIncreasingWhenNewQuantityIsAdded()
        {
            // Arrange - create some test products
            Product p1 = new Product {ProductId = 1, Name = "P1"};
            Product p2 = new Product {ProductId = 2, Name = "P2"};
            Cart target = new Cart();

            // Act
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            target.AddItem(p1, 10);
            CartLine[] results = target.Lines.OrderBy(c => c.Product.ProductId).ToArray();

            // Assert
            Assert.AreEqual(results.Length, 2);
            Assert.AreEqual(results[0].Quantity, 11);
            Assert.AreEqual(results[1].Quantity, 1);
        }

        [Test]
        public void ExistingProuctIsRemoved()
        {
            // Arrange - create some test products
            Product p1 = new Product {ProductId = 1, Name = "P1"};
            Product p2 = new Product {ProductId = 2, Name = "P2"};
            Product p3 = new Product {ProductId = 3, Name = "P3"};

            Cart target = new Cart();

            target.AddItem(p1, 1);
            target.AddItem(p2, 3);
            target.AddItem(p3, 5);
            target.AddItem(p2, 1);

            // Act
            target.RemoveLine(p2);

            // Assert
            Assert.AreEqual(target.Lines.Count(c => c.Product == p2), 0);
            Assert.AreEqual(target.Lines.Count(), 2);
        }
    }
}