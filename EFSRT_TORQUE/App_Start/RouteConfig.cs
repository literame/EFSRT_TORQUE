using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace EFSRT_TORQUE
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // Ruta para la vista de inicio de sesión

            routes.MapRoute(
                name: "Login",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Usuarios", action = "Login", id = UrlParameter.Optional }
    );
            routes.MapRoute(
             name: "Registro",
             url: "{controller}/{action}/{id}",
             defaults: new { controller = "Usuarios", action = "Registro", id = UrlParameter.Optional }
 );

            // Ruta predeterminada
            routes.MapRoute(
                name: "Index",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }

    }
}
