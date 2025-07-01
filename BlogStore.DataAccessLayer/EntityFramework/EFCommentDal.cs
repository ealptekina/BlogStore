using BlogStore.DataAccessLayer.Abstract;
using BlogStore.DataAccessLayer.Context;
using BlogStore.DataAccessLayer.Dtos;
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

        public List<CommentWithArticleDto> GetCommentsWithArticleTitles()
        {
            var result = (from c in _context.Set<Comment>()
                          join a in _context.Set<Article>()
                          on c.ArticleId equals a.ArticleId into articlesGroup
                          from a in articlesGroup.DefaultIfEmpty()
                          select new CommentWithArticleDto
                          {
                              CommentId = c.CommentId,
                              UserNameSurname = c.UserNameSurname,
                              CommentDate = c.CommentDate,
                              CommentDetail = c.CommentDetail,
                              IsValid = c.IsValid,
                              ArticleTitle = a != null ? a.Title : null
                          }).ToList();

            return result;
        }

        
    }
}
