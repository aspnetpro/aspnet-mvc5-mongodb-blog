using System;
using System.Collections.Generic;
using System.Linq;

namespace MongoBlog.Models
{
    public class PagingOptions
    {
        private int _page;
        public int Page
        {
            get { return (_page <= 0) ? 1 : _page; }
            set { _page = value; }
        }

        public int Size { get; set; }
    }
}
