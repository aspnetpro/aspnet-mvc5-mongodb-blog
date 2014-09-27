using MongoRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MongoBlog.Models.Entities
{
    [CollectionName("posts")]
    public class Post : IEntity
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Body { get; set; }
        public int CommentsCount { get; set; }
        public Category Category { get; set; }
        public DateTime PublishedOn { get; set; }

        public IEnumerable<string> Tags
        {
            get { return _tags ?? (_tags = Enumerable.Empty<string>()); }
            set { _tags = value; }
        }
        IEnumerable<string> _tags;

        public Post()
        {
            PublishedOn = DateTime.Now;
        }
    }
}