using MongoRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MongoBlog.Models.Entities
{
    [CollectionName("comments")]
    public class Comment : Entity
    {
        public string PostId { get; set; }
        public string Author { get; set; }
        public string Email { get; set; }
        public string Body { get; set; }
        public DateTime PublishedOn { get; set; }

        public Comment()
        {
            PublishedOn = DateTime.Now;
        }
    }
}