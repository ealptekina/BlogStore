using BlogStore.DataAccessLayer.Abstract;
using BlogStore.DataAccessLayer.Context;
using BlogStore.DataAccessLayer.Repositories;
using BlogStore.EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogStore.DataAccessLayer.EntityFramework
{
    public class EFCommentDal : GenericRepository<Comment>, ICommentDal
    {
        private readonly BlogContext _context;
        public EFCommentDal(BlogContext context) : base(context)
        {
            _context = context;
        }

        public List<Comment> GetCommentsByArticle(int id)
        {
            var values = _context.Comments.Include(x => x.AppUser).Include(y => y.Article).Where(z => z.ArticleId == id).ToList();
            return values;
        }
    }
}
