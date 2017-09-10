namespace SportsStore.WebUI
{
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Infrastructure.Binders;
    using SportsStore.Models.Entities;

    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AutoMapperConfig.RegisterAllMappings();
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            ModelBinders.Binders.Add(typeof(Cart), new CartModelBinder());
        }
    }
}