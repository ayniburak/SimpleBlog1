using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SimpleBlog2
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            var namespaces = new[] { typeof(PostsController).Namespace };
            routes.MapRoute("Home", "", new { controller = "Posts", action = "Index" },namespaces);
            //routes.MapRoute("Home", "", new { controller = "Posts", action = "Index" },new[]{"SimpleBlog2.Controllers"}); böyle yaparsakda olur namespaces tanımlamaya gerek kalmaz(birden fazla şeyde yan yana tanımlanabilir.)
            routes.MapRoute("Login", "login", new { controller = "Auth", action = "Login" },namespaces);
            routes.MapRoute("Logout", "logout", new { controller = "Auth", action = "Logout" }, namespaces);


        }
    }
}
