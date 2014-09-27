using System.Web.Mvc;

namespace MongoBlog.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get { return "Admin"; }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            string[] namespaces = new string[] { "MongoBlog.Areas.Admin.Controllers" };


            context.MapRoute(
                name: "Admin.Dashboard.Index",
                url: "admin/dashboard",
                defaults: new { controller = "Dashboard", action = "Index" },
                namespaces: namespaces
            );
            context.MapRoute(
                name: "Admin.Posts.Index",
                url: "admin/posts",
                defaults: new { controller = "Posts", action = "Index" },
                namespaces: namespaces
            );
            context.MapRoute(
                name: "Admin.Posts.List",
                url: "admin/posts/list",
                defaults: new { controller = "Posts", action = "List" },
                namespaces: namespaces
            );
            context.MapRoute(
                name: "Admin.Posts.Add",
                url: "admin/posts/add",
                defaults: new { controller = "Posts", action = "Add" },
                namespaces: namespaces
            );
            context.MapRoute(
                name: "Admin.Posts.Edit",
                url: "admin/posts/edit/{postId}",
                defaults: new { controller = "Posts", action = "Edit" },
                namespaces: namespaces
            );
            context.MapRoute(
                name: "Admin.Posts.Save",
                url: "admin/posts/save",
                defaults: new { controller = "Posts", action = "Save" },
                namespaces: namespaces
            );
            context.MapRoute(
                name: "Admin.Posts.Delete",
                url: "admin/posts/delete/{postId}",
                defaults: new { controller = "Posts", action = "Delete" },
                namespaces: namespaces
            );

            context.MapRoute(
                "Admin.Autocomplete.Tags",
                "admin/autocomplete/tags",
                new { controller = "Autocomplete", action = "Tags" },
                namespaces
            );
            context.MapRoute(
                "Admin.Autocomplete.Categories",
                "admin/autocomplete/categories",
                new { controller = "Autocomplete", action = "Categories" },
                namespaces
            );

            context.MapRoute(
                name: "Admin.GridFS.Upload",
                url: "admin/gridfs/upload",
                defaults: new { controller = "GridFS", action = "Upload" },
                namespaces: namespaces
            );
            context.MapRoute(
                name: "Admin.GridFS.GetUploadedImages",
                url: "admin/gridfs/get-uploaded-images",
                defaults: new { controller = "GridFS", action = "GetUploadedImages" },
                namespaces: namespaces
            );

            context.MapRoute(
                "Admin.Auth.LogOn",
                "admin",
                new { controller = "Auth", action = "LogOn" },
                namespaces
            );
            context.MapRoute(
                "Admin.Auth.LogOut",
                "admin/logout",
                new { controller = "Auth", action = "LogOut" },
                namespaces
            );
        }
    }
}