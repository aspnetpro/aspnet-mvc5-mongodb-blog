using MongoBlog.Models;
using MongoBlog.Models.Entities;
using MongoDB.Driver.Builders;
using MongoRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;

namespace MongoBlog.Controllers
{
    public class PostsController : Controller
    {
        const int PAGE_SIZE = 1;

        [OutputCache(Duration = 10 /* segundos */)]
        public ActionResult GetAll(PagingOptions options)
        {
            //descomentar para exibir a página de erro
            //throw new Exception();

            var mongo = new MongoRepository<Post>();
            var posts = mongo.OrderByDescending(x => x.PublishedOn);

            options.Size = PAGE_SIZE;
            var model = new PagedList<Post>(posts, options.Page, options.Size);

            return View("List", model);
        }

        [OutputCache(Duration = 60)]
        public ActionResult GetByCategory(string category, PagingOptions options)
        {
            options.Size = PAGE_SIZE;

            if (String.IsNullOrWhiteSpace(category))
            {
                return View("List", new PagedList<Post>(Enumerable.Empty<Post>(), options.Page, options.Size, 0));
            }

            var mongo = new MongoRepository<Post>();

            var posts = mongo
                .Where(x => x.Category.Permalink == category)
                .OrderByDescending(x => x.PublishedOn);

            var model = new PagedList<Post>(posts, options.Page, options.Size);

            return View("List", model);
        }

        [OutputCache(Duration = 60)]
        public ActionResult GetByTag(string tag, PagingOptions options)
        {
            if (String.IsNullOrWhiteSpace(tag))
            {
                return View("List", Enumerable.Empty<Post>());
            }

            var mongo = new MongoRepository<Post>();

            var posts = mongo
                .Where(x => x.Tags.Contains(tag))
                .OrderByDescending(x => x.PublishedOn);

            options.Size = PAGE_SIZE;
            var model = new PagedList<Post>(posts, options.Page, options.Size);

            return View("List", model);
        }

        [OutputCache(Duration = 60)]
        public ActionResult Search(string term, PagingOptions options)
        {
            var mongo = new MongoRepository<Post>();

            if (String.IsNullOrWhiteSpace(term))
            {
                var postsAll = mongo.OrderByDescending(x => x.PublishedOn);
                return View("List", new PagedList<Post>(postsAll, options.Page, options.Size));
            }

            var posts = mongo
                .Where(x =>
                    x.Title.Contains(term) ||
                    x.Summary.Contains(term) ||
                    x.Body.Contains(term)
                )
                .OrderByDescending(x => x.PublishedOn);

            options.Size = PAGE_SIZE;
            var model = new PagedList<Post>(posts, options.Page, options.Size);

            return View("List", model);
        }

        public ActionResult Details(string id)
        {
            //pega o cache
            var cache = MemoryCache.Default;

            //obtem o post do cache
            var model = cache.Get(id) as Post;
            //se nao estiver no cache
            if (model == null)
            {
                var posts = new MongoRepository<Post>();
                
                model = posts.GetById(id);
                if (model == null)
                {
                    return HttpNotFound();
                }

                //seta a política de expiração
                var policy =new CacheItemPolicy();
                policy.AbsoluteExpiration = DateTime.Now.AddHours(1);
                //busca o post no banco e adiciona no cache
                cache.Add(id, model, policy);
            }

            return View(model);
        }

        [OutputCache(Duration=60)]
        public ActionResult LoadComments(string id, PagingOptions options)
        {
            var mongo = new MongoRepository<Comment>();

            var comments = mongo
                .Where(x => x.PostId == id)
                .OrderByDescending(x => x.PublishedOn);

            options.Size = PAGE_SIZE;
            var model = new PagedList<Comment>(comments, options.Page, options.Size);

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