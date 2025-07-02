using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogStore.EntityLayer.Entities
{
    public class ArticleTag
    {
        public int ArticleId { get; set; }
        public Article Article { get; set; }

        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
