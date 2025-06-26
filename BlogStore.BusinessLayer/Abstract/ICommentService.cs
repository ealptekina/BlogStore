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
        public List<Comment> TGetCommentsByArticle(int id);
    }
}
