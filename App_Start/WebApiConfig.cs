using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Proyectoanalisis_.App_Start
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Configurar servicios de api web
             config.EnableCors();
            // rutas de api web
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute
                (
                name: "DefaultApi",
                routeTemplate:"api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
                );

        }


    }
}