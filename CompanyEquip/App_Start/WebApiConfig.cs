using System;
using System.Net.Http.Headers;
using System.Web.Http;

namespace CompanyEquip.App_Start
{
    /* 
     * WebApiConfig — only applicable if you are using Web API. It can be used to configure Web API-specific routes, 
     * any Web API settings and Web API services.
     */
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
        }
    }
}