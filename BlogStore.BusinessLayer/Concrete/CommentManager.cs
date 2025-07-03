using BlogStore.BusinessLayer.Abstract;
using BlogStore.DataAccessLayer.Abstract;
using BlogStore.DataAccessLayer.Context;
using BlogStore.DataAccessLayer.Dtos;
using BlogStore.EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogStore.BusinessLayer.Concrete
{
    public class CommentManager : ICommentService

    {
        private readonly ICommentDal _commentDal;
        private readonly BlogContext _context;

        public CommentManager(ICommentDal commentDal, BlogContext context)
        {
            _commentDal = commentDal;
            _context = context;
        }

        public void TDelete(int id)
        {
            _commentDal.Delete(id);
        }

        public List<Comment> TGetAll()
        {
            return _commentDal.GetAll();
        }

        public Comment TGetById(int id)
        {
            return _commentDal.GetById(id);
        }

        public List<Comment> TGetCommentsByArticle(int id)
        {
            return _commentDal.GetCommentsByArticle(id);
        }

        public void TInsert(Comment entity)
        {
            _commentDal.Insert(entity);
        }

        public void TUpdate(Comment entity)
        {
            _commentDal.Update(entity);
        }

        public void CommentAdd(Comment c)
        {
            if (c.CommentDetail.Length <= 4 || c.CommentDetail.Length >= 501 || string.IsNullOrWhiteSpace(c.UserNameSurname) || string.IsNullOrWhiteSpace(c.AppUserId))
            {
                throw new ArgumentException("Yorum bilgileri geçersiz.");
            }
            _commentDal.Insert(c);
        }

        public List<CommentWithArticleDto> GetCommentsWithArticleTitles()
        {
            return _commentDal.GetCommentsWithArticleTitles();
        }

        public List<Comment> GetCommentsByArticle(int articleId)
        {
            return _context.Comments
                           .Include(c => c.AppUser) // Kullanıcı bilgisini de getir
                           .Where(c => c.ArticleId == articleId)
                           .OrderByDescending(c => c.CommentDate)
                           .ToList();
        }
    }
}
