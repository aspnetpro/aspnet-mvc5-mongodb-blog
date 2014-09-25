using MongoBlog.Models.Entities;
using MongoDB.Driver.Builders;
using MongoRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MongoBlog.Controllers
{
    public class PostsController : Controller
    {
        public ActionResult GetAll()
        {
            //descomentar para exibir a página de erro
            //throw new Exception();

            var posts = new MongoRepository<Post>();
            var model = posts.ToList();
            return View("List", model);
        }

        public ActionResult GetByCategory(string category)
        {
            if (String.IsNullOrWhiteSpace(category))
            {
                return View("List", Enumerable.Empty<Post>());
            }

            var posts = new MongoRepository<Post>();

            var model = posts
                .Where(x => x.Category.Permalink == category)
                .OrderByDescending(x => x.PublishedOn);

            return View("List", model);
        }

        public ActionResult GetByTag(string tag)
        {
            if (String.IsNullOrWhiteSpace(tag))
            {
                return View("List", Enumerable.Empty<Post>());
            }

            var posts = new MongoRepository<Post>();

            var model = posts
                .Where(x => x.Tags.Contains(tag))
                .OrderByDescending(x => x.PublishedOn);

            return View("List", model);
        }

        public ActionResult Search(string term)
        {
            var posts = new MongoRepository<Post>();

            if (String.IsNullOrWhiteSpace(term))
            {
                return View("List", posts.ToList());
            }

            var model = posts
                .Where(x =>
                    x.Title.Contains(term) ||
                    x.Summary.Contains(term) ||
                    x.Body.Contains(term)
                )
                .OrderByDescending(x => x.PublishedOn);

            return View("List", model);
        }

        public ActionResult Details(string id)
        {
            var posts = new MongoRepository<Post>();

            var model = posts.GetById(id);
            if (model == null)
            {
                return HttpNotFound();
            }

            return View(model);
        }

        public ActionResult LoadComments(string id)
        {
            var comments = new MongoRepository<Comment>();

            var model = comments
                .Where(x => x.PostId == id)
                .OrderByDescending(x => x.PublishedOn);

            return PartialView("_Comments", model);
        }

        [HttpPost]
        public ActionResult AddComment(Comment comment)
        {
            //add comment
            var comments = new MongoRepository<Comment>();
            comments.Add(comment);

            // +1 to post "CommentsCount"
            var posts = new MongoRepository<Post>();
            var query = Query<Post>.EQ(x => x.Id, comment.PostId);
            var update = Update<Post>.Inc(x => x.CommentsCount, 1);
            posts.Collection.Update(query, update);

            return RedirectToAction("Details", new { id = comment.PostId });
        }
    }
}