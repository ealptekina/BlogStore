using BlogStore.DataAccessLayer.Abstract;
using BlogStore.DataAccessLayer.Context;
using BlogStore.DataAccessLayer.Repositories;
using BlogStore.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogStore.DataAccessLayer.EntityFramework
{
    public class EFTagDal : GenericRepository<Tag>, ITagDal
    {
        private readonly BlogContext _context;
        public EFTagDal(BlogContext context) : base(context)
        {
            _context = context;
        }

        public List<Tag> GetTagsByArticleId(int articleId)
        {
            return _context.ArticleTags
        .Where(at => at.ArticleId == articleId)
        .Select(at => at.Tag)
        .ToList();
        }
    }
}
