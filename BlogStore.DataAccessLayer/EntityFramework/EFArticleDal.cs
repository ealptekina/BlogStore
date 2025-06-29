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
    public class EFArticleDal : GenericRepository<Article>, IArticleDal
    {
        private readonly BlogContext _context;
        public EFArticleDal(BlogContext context) : base(context)
        {
            _context = context;
        }

        public AppUser GetAppUserByArticleId(int id)
        {
            string userId = _context.Articles.Where(x => x.ArticleId == id).Select(y => y.AppUserId).FirstOrDefault();
            var userValue = _context.Users.Where(x => x.Id == userId).FirstOrDefault();
            return userValue;
        }

        public Article GetArticleByIdWithIncludes(int id)
        {
            return _context.Articles
                    .Include(x => x.AppUser)
                    .Include(x => x.Category)
                    .Include(x => x.Comments)
                    .FirstOrDefault(x => x.ArticleId == id);
        }

        public List<Article> GetArticlesByAppUser(string id)
        {
            return _context.Articles.Where(x => x.AppUserId == id).ToList();
        }

        public List<Article> GetArticleWithCategories()
        {
            return _context.Articles.Include(x => x.Category).ToList();
        }

        public List<Article> GetTop3PopularArticles()
        {
            var values = _context.Articles.OrderByDescending(x => x.ArticleId).Take(3).ToList();
            return values;
        }
    }
}
