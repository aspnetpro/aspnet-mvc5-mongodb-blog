using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MongoBlog.Areas.Admin.Models
{
    public class PostModel
    {
        public string PostId { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string Summary { get; set; }
        public string Body { get; set; }
        public string Tags { get; set; }
    }
}