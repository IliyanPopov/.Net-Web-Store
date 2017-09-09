namespace SportsStore.WebUI.Controllers
{
    using System.Web.Mvc;
    using Infrastructure.Contracts;
    using ViewModels;

    public class AccountController : Controller
    {
        private readonly IAuthProvider authProvider;

        public AccountController(IAuthProvider auth)
        {
            this.authProvider = auth;
        }

        public ViewResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (this.ModelState.IsValid)
            {
                if (this.authProvider.Authenticate(model.UserName,
                    model.Password))
                {
                    return Redirect(returnUrl ?? this.Url.Action("Index",
                                        "Admin"));
                }
                this.ModelState.AddModelError("", "Incorrect username or                    password");
                return View();
            }
            return View();
        }
    }
}