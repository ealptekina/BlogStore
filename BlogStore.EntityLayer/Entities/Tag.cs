﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogStore.EntityLayer.Entities
{
    public class Tag
    {
        public int TagId { get; set; }
        public string Title { get; set; }

        public ICollection<ArticleTag> ArticleTags { get; set; } = new List<ArticleTag>();
    }
}
