using MongoBlog.Models.Entities;
using MongoRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MongoBlog.Controllers
{
    public class GridFsController : Controller
    {
        public ActionResult GetFile(string fileName)
        {
            var posts = new MongoRepository<Post>();
            var gridFs = posts.Collection.Database.GridFS;
            var file = gridFs.FindOne(fileName);

            var stream = file.OpenRead();

            return File(stream, file.ContentType);
        }
    }
}
