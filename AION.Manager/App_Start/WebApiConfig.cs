using AION.Manager.Attributes;
using System.Web.Http;

namespace AION.Manager
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //setup to catch all global exception
            config.Filters.Add(new ExceptionHandlingAttribute());
        }
    }
}
