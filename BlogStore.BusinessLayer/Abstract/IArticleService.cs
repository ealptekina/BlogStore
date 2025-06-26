using BlogStore.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogStore.BusinessLayer.Abstract
{
    public interface IArticleService : IGenericService<Article>
    {
        public List<Article> TGetArticleWithCategories();
        public AppUser TGetAppUserByArticleId(int id);
        public List<Article> TGetTop3PopularArticles();
        public List<Article> TGetArticlesByAppUser(string id);
    }
}
