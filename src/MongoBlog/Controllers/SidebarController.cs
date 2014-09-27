using MongoBlog.Models.Entities;
using MongoBlog.Models.ViewModel;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MongoBlog.Controllers
{
    public class SidebarController : Controller
    {
        public ActionResult Categories()
        {
            //mongo shell
            //db.posts.aggregate([
            //    { 
            //        "$group" : { 
            //            "_id" : { 
            //                "category" : "$Category.Name", 
            //                "permalink" : "$Category.Permalink" 
            //            }, 
            //            "posts_count" : { 
            //                "$sum" : 1 
            //            } 
            //        } 
            //    }
            //])

            var group = new BsonDocument
            {
                { 
                    "$group",
                    new BsonDocument
                    {
                        {
                            "_id",
                            new BsonDocument
                            {
                                { "name", "$Category.Name" },
                                { "permalink", "$Category.Permalink" }
                            }
                        },
                        {
                            "posts_count",
                            new BsonDocument
                            {
                                { "$sum", 1 }
                            }
                        }
                    }
                }
            };

            AggregateArgs args = new AggregateArgs();
            args.Pipeline = new List<BsonDocument> { group };

            var posts = new MongoRepository<Post>();
            var result = posts.Collection.Aggregate(args);

            List<CategoryItem> model = new List<CategoryItem>();
            foreach (var item in result)
            {
                model.Add(new CategoryItem
                {
                    Name = item["_id"]["name"].AsString,
                    Permalink = item["_id"]["permalink"].AsString,
                    PostsCount = item["posts_count"].AsInt32
                });
            }

            return PartialView("_Categories", model);
        }

        public ActionResult Tags()
        {
            //mongo shell
            //db.posts.aggregate([
            //    { 
            //        "$unwind": "$Tags"
            //    },
            //    {
            //        "$group": {
            //            "_id": "$Tags"
            //        }
            //    }
            //])

            var unwind = new BsonDocument
            {
                { "$unwind", "$Tags" }
            };
            var group = new BsonDocument
            {
                { 
                    "$group",
                    new BsonDocument
                    {
                        { "_id", "$Tags" }
                    }
                }
            };

            AggregateArgs args = new AggregateArgs();
            args.Pipeline = new List<BsonDocument> { 
                unwind,
                group 
            };

            var posts = new MongoRepository<Post>();
            var result = posts.Collection.Aggregate(args);

            List<string> model = new List<string>();
            foreach (var item in result)
            {
                model.Add(item["_id"].AsString);
            }

            return PartialView("_Tags", model);
        }
    }
}