using BlogStore.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogStore.DataAccessLayer.Abstract
{
    public interface IArticleDal : IGenericDal<Article>
    {
        List<Article> GetArticleWithCategories();
        public AppUser GetAppUserByArticleId(int id);
        List<Article> GetTop3PopularArticles();
        List<Article> GetArticlesByAppUser(string id);

        public Article GetArticleByIdWithIncludes(int id);
        Article Get(Expression<Func<Article, bool>> filter);

    }
}
