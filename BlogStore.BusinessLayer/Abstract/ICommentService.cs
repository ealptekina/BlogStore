using BlogStore.DataAccessLayer.Dtos;
using BlogStore.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogStore.BusinessLayer.Abstract
{
    public interface ICommentService: IGenericService<Comment>
    {
        void CommentAdd(Comment comment);
        List<Comment> TGetCommentsByArticle(int id);

        List<CommentWithArticleDto> GetCommentsWithArticleTitles();

        List<Comment> GetCommentsByArticle(int articleId);

    }
}
