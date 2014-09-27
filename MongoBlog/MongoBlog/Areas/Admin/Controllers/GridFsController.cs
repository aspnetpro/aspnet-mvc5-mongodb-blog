using MongoBlog.Models.Entities;
using MongoDB.Driver.GridFS;
using MongoRepository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MongoBlog.Areas.Admin.Controllers
{
    [Authorize]
    public class GridFsController : Controller
    {
        public ActionResult Upload(HttpPostedFileBase file)
        {
            //upload file
            var posts = new MongoRepository<Post>();
            var gridFs = posts.Collection.Database.GridFS;

            string newFileName = Path.GetRandomFileName().ToSlug();

            MongoGridFSCreateOptions createOption = new MongoGridFSCreateOptions
            {
                ContentType = file.ContentType,
                UploadDate = DateTime.Now
            };
            MongoGridFSFileInfo gridFsInfo = gridFs.Upload(file.InputStream, newFileName, createOption);

            var model = new
            {
                filelink = Url.RouteUrl("GridFs.GetFile", new { fileName = gridFsInfo.Name }),
                title = newFileName
            };

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetUploadedImages()
        {
            var posts = new MongoRepository<Post>();
            var gridFs = posts.Collection.Database.GridFS;

            List<object> model = new List<object>();
            foreach (var file in gridFs.FindAll())
            {
                model.Add(new
                { 
                    thumb = Url.RouteUrl("GridFs.GetFile", new { fileName = file.Name }), 
                    image = Url.RouteUrl("GridFs.GetFile", new { fileName = file.Name }), 
                    title = file.Name, 
                    folder = "default" 
                });
            }

            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}
