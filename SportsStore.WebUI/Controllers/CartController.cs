namespace SportsStore.WebUI.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Data.Contracts;
    using SportsStore.Models.Entities;
    using ViewModels;

    public class CartController : Controller
    {
        private readonly IGenericRepository<Product> _productRepository;

        public CartController(IGenericRepository<Product> repo)
        {
            this._productRepository = repo;
        }

        public RedirectToRouteResult AddToCart(Cart cart, int productId, string returnUrl)
        {
            Product product = this._productRepository.All
                .FirstOrDefault(p => p.ProductId == productId);
            if (product != null)
            {
                cart.AddItem(product, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart, int productId, string returnUrl)
        {
            Product product = this._productRepository.All
                .FirstOrDefault(p => p.ProductId == productId);
            if (product != null)
            {
                cart.RemoveLine(product);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public ViewResult Index(Cart cart, string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }

        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }

        public ViewResult Checkout()
        {
            return View(new ShippingDetails());
        }
    }
}