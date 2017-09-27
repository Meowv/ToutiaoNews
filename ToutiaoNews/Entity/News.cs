using System;
using System.Collections.Generic;
using System.Text;

namespace ToutiaoNews.Entity
{
    public class News
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Source { get; set; }

        public string Logo { get; set; }

        public string Labels { get; set; }

        public string Category { get; set; }

        public DateTime Pubdate { get; set; }

        public string Detail { get; set; }
    }
}
