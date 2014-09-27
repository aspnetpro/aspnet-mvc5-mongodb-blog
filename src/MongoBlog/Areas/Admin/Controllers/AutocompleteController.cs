using MongoBlog.Models.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MongoBlog.Areas.Admin.Controllers
{
    [Authorize]
    public class AutocompleteController : Controller
    {
        [OutputCache(Duration = 60)]
        public ActionResult Categories(string term)
        {
            var group = new BsonDocument 
            { 
                { "$group", 
                    new BsonDocument 
                    { 
                        { "_id", "$Category.Name" }
                    } 
                } 
            };

            var mongo = new MongoRepository<Post>();

            AggregateArgs args = new AggregateArgs();
            args.Pipeline = new List<BsonDocument> { group };
            var result = mongo.Collection.Aggregate(args);

            return PrepareResult(term, result);
        }

        [OutputCache(Duration=60)]
        public ActionResult Tags(string term)
        {
            var unwind = new BsonDocument 
            { 
                { "$unwind", "$Tags" } 
            };
            var group = new BsonDocument 
            { 
                { "$group", 
                    new BsonDocument 
                    { 
                        { "_id", "$Tags" }
                    } 
                } 
            };

            var mongo = new MongoRepository<Post>();

            AggregateArgs args = new AggregateArgs();
            args.Pipeline = new List<BsonDocument> { unwind, group };
            var result = mongo.Collection.Aggregate(args);

            return PrepareResult(term, result);
        }

        [NonAction]
        private ActionResult PrepareResult(string term, IEnumerable<BsonDocument> result)
        {
            List<string> model = new List<string>();
            foreach (var item in result)
            {
                model.Add(item["_id"].ToString());
            }

            model = model
                .Where(x => x.Contains(term))
                .ToList();

            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}
