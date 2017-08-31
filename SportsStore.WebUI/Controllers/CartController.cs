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

        public RedirectToRouteResult AddToCart(int productId, string returnUrl)
        {
            Product product = this._productRepository.All
                .FirstOrDefault(p => p.ProductId == productId);
            if (product != null)
            {
                GetCart().AddItem(product, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(int productId, string returnUrl)
        {
            Product product = this._productRepository.All
                .FirstOrDefault(p => p.ProductId == productId);
            if (product != null)
            {
                GetCart().RemoveLine(product);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        private Cart GetCart()
        {
            Cart cart = (Cart)this.Session["Cart"];
            if (cart == null)
            {
                cart = new Cart();
                this.Session["Cart"] = cart;
            }
            return cart;
        }

        public ViewResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = GetCart(),
                ReturnUrl = returnUrl
            });
        }
    }
}