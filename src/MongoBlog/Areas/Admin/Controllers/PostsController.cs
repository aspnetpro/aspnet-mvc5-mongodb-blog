using MongoBlog.Areas.Admin.Models;
using MongoBlog.Models.Entities;
using MongoRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Web;
using System.Web.Mvc;

namespace MongoBlog.Areas.Admin.Controllers
{
    [Authorize]
    public class PostsController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List(jQueryDataTableRequestModel request)
        {
            var repository = new MongoRepository<Post>();
            IQueryable<Post> posts = repository.OrderByDescending(x => x.PublishedOn);

            if (String.IsNullOrWhiteSpace(request.sSearch) == false)
            {
                posts = posts.Where(x =>
                        x.Title.Contains(request.sSearch) ||
                        x.Summary.Contains(request.sSearch) ||
                        x.Tags.Contains(request.sSearch)
                    );
            }

            int total = posts.Count();
            posts = posts.Skip(request.iDisplayStart * request.iDisplayLength).Take(request.iDisplayLength);

            var model = new jQueryDataTableResponseModel
            {
                sEcho = request.sEcho,
                iTotalRecords = total,
                iTotalDisplayRecords = total,
                aaData = from r in posts.ToList()
                         select new
                         {
                             PostId = r.Id,
                             Title = r.Title,
                             PublishedOn = r.PublishedOn.ToString("dd/MM/yyyy"),
                             EditUrl = Url.RouteUrl("Admin.Posts.Edit", new { postId = r.Id }),
                             DeleteUrl = Url.RouteUrl("Admin.Posts.Delete", new { postId = r.Id })
                         }
            };

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Add()
        {
            var model = new PostModel();
            return View("AddOrEdit", model);
        }

        public ActionResult Edit(string postId)
        {
            var repository = new MongoRepository<Post>();
            
            var post = repository.GetById(postId);
            if (post == null)
            {
                return HttpNotFound();
            }

            var model = PreparePostModel(post);

            return View("AddOrEdit", model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Save(PostModel model)
        {
            if (String.IsNullOrWhiteSpace(model.Title))
            {
                return View("AddOrEdit", model);
            }

            Post newPost = new Post();
            newPost.Id = model.PostId ?? model.Title.ToSlug();
            newPost.Title = model.Title;
            newPost.Summary = model.Summary;
            newPost.Body = model.Body;
            newPost.Category = new Category
            {
                Name = model.Category,
                Permalink = model.Category.ToSlug()
            };

            if (String.IsNullOrWhiteSpace(model.Tags) == false)
            {
                newPost.Tags = model.Tags
                    .Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                    .ToList();
            }

            try
            {
                var repository = new MongoRepository<Post>();
                repository.Collection.Save(newPost);

                //remove do cache
                MemoryCache.Default.Remove(newPost.Id);
            }
            catch (Exception)
            {
                Error("Your post cannot saved");
            }

            Success("Your post has been saved");

            return RedirectToAction("Edit", new { postId = newPost.Id });
        }

        public ActionResult Delete(string postId)
        {
            var repository = new MongoRepository<Post>();

            var post = repository.GetById(postId);
            if (post == null)
            {
                return HttpNotFound();
            }

            var model = PreparePostModel(post);

            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(PostModel model)
        {
            var repository = new MongoRepository<Post>();
            repository.Delete(model.PostId);

            Success("Post deleted!");

            return RedirectToAction("Index");
        }

        [NonAction]
        private static PostModel PreparePostModel(Post post)
        {
            var model = new PostModel();
            model.PostId = post.Id;
            model.Title = post.Title;
            model.Summary = post.Summary;
            model.Body = post.Body;
            model.Category = post.Category.Name;
            model.Tags = String.Join(",", post.Tags);
            return model;
        }
    }
}