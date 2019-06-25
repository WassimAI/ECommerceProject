﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ECommerceProject
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            /*For home default page*/
            routes.MapRoute("Default", "", new { controller = "Home", action = "Index" }, new[] { "ECommerceProject.Controllers" });

            /*For all pages in main area*/
            routes.MapRoute(
                name: "Defaults",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
