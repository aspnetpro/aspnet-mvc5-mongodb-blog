using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MongoBlog.Models.ViewModel
{
    public class CategoryItem
    {
        public string Name { get; set; }
        public string Permalink { get; set; }
        public int PostsCount { get; set; }
    }
}