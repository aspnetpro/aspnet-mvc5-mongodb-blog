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
            string[] namespaces = new string[] { "MongoBlog.Controllers" };
            
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Posts.GetAll",
                url: "",
                defaults: new { controller = "Posts", action = "GetAll" },
                namespaces: namespaces
            );
            routes.MapRoute(
                name: "Posts.GetByCategory",
                url: "category/{category}",
                defaults: new { controller = "Posts", action = "GetByCategory", category = UrlParameter.Optional },
                namespaces: namespaces
            );
            routes.MapRoute(
                name: "Posts.GetByTag",
                url: "tag/{tag}",
                defaults: new { controller = "Posts", action = "GetByTag", tag = UrlParameter.Optional },
                namespaces: namespaces
            );
            routes.MapRoute(
                name: "Posts.Search",
                url: "search",
                defaults: new { controller = "Posts", action = "Search" },
                namespaces: namespaces
            );
            routes.MapRoute(
                name: "Posts.Details",
                url: "post/{id}",
                defaults: new { controller = "Posts", action = "Details" },
                namespaces: namespaces
            );
            routes.MapRoute(
                name: "Posts.LoadComments",
                url: "post/{id}/load-comments",
                defaults: new { controller = "Posts", action = "LoadComments" },
                namespaces: namespaces
            );
            routes.MapRoute(
                name: "Posts.AddComment",
                url: "post/{postId}/add-comment",
                defaults: new { controller = "Posts", action = "AddComment" },
                namespaces: namespaces
            );

            routes.MapRoute(
                name: "Sidebar.Categories",
                url: "sidebar/categories",
                defaults: new { controller = "Sidebar", action = "Categories" },
                namespaces: namespaces
            );
            routes.MapRoute(
                name: "Sidebar.Tags",
                url: "sidebar/tags",
                defaults: new { controller = "Sidebar", action = "Tags" },
                namespaces: namespaces
            );

            routes.MapRoute(
                name: "Contact.Index",
                url: "contact",
                defaults: new { controller = "Contact", action = "Index" },
                namespaces: namespaces
            );

            routes.MapRoute(
                name: "GridFs.GetFile",
                url: "gridfs/{fileName}",
                defaults: new { controller = "GridFs", action = "GetFile" },
                namespaces: namespaces
            );

            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            //);
        }
    }
}
