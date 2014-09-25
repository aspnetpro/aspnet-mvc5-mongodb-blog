using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MongoBlog
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Posts.GetAll",
                url: "",
                defaults: new { controller = "Posts", action = "GetAll" }
            );
            routes.MapRoute(
                name: "Posts.GetByCategory",
                url: "category/{category}",
                defaults: new { controller = "Posts", action = "GetByCategory", category = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Posts.GetByTag",
                url: "tag/{tag}",
                defaults: new { controller = "Posts", action = "GetByTag", tag = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Posts.Search",
                url: "search",
                defaults: new { controller = "Posts", action = "Search" }
            );
            routes.MapRoute(
                name: "Posts.Details",
                url: "post/{id}",
                defaults: new { controller = "Posts", action = "Details" }
            );
            routes.MapRoute(
                name: "Posts.LoadComments",
                url: "post/{id}/load-comments",
                defaults: new { controller = "Posts", action = "LoadComments" }
            );
            routes.MapRoute(
                name: "Posts.AddComment",
                url: "post/{postId}/add-comment",
                defaults: new { controller = "Posts", action = "AddComment" }
            );

            routes.MapRoute(
                name: "Sidebar.Categories",
                url: "sidebar/categories",
                defaults: new { controller = "Sidebar", action = "Categories" }
            );
            routes.MapRoute(
                name: "Sidebar.Tags",
                url: "sidebar/tags",
                defaults: new { controller = "Sidebar", action = "Tags" }
            );

            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            //);
        }
    }
}
