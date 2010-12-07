using System.Web.Mvc;
using System.Web.Routing;
using Naak.HtmlRules;

namespace Naak.ExampleMvcUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_BeginRequest()
        {
            DependencyRegistrar.EnsureDependenciesRegistered();

            string s = Request.Path;
        }
    }
}