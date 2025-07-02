using BlogStore.BusinessLayer.Abstract;
using BlogStore.DataAccessLayer.Abstract;
using BlogStore.DataAccessLayer.EntityFramework;
using BlogStore.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogStore.BusinessLayer.Concrete
{
    public class ArticleManager : IArticleService
    {
        private readonly IArticleDal _articleDal;

        public ArticleManager(IArticleDal articleDal)
        {
            _articleDal = articleDal;
        }

        public AppUser TGetAppUserByArticleId(int id)
        {
            return _articleDal.GetAppUserByArticleId(id);
        }

        public void TDelete(int id)
        {
            _articleDal.Delete(id);
        }

        public List<Article> TGetAll()
        {
            return _articleDal.GetAll();
        }

        public List<Article> TGetArticleWithCategories()
        {
            return _articleDal.GetArticleWithCategories();
        }

        public Article TGetById(int id)
        {
            return _articleDal.GetById(id);
        }

        public void TInsert(Article entity)
        {
            // Başlık 10-100 karakter arasında olmalı,
            // Açıklama boş olmamalı,
            // Görsel URL'si "a" harfi içermelidir (örnek iş kuralı)
            if (entity.Title.Length >= 10 && entity.Title.Length <= 100 && entity.Description != "" && entity.ImageUrl.Contains("a"))
            {
                _articleDal.Insert(entity);
            }
            else
            {
                //hata mesajı
            }
        }

        public void TUpdate(Article entity)
        {
            _articleDal.Update(entity);

        }

        public List<Article> GetTop3PopularArticles()
        {
            return _articleDal.GetTop3PopularArticles();
        }

        public List<Article> TGetTop3PopularArticles()
        {
            return _articleDal.GetTop3PopularArticles();
        }

        public List<Article> TGetArticlesByAppUser(string id)
        {
            return _articleDal.GetArticlesByAppUser(id);
        }

        public Article TGetArticleByIdWithIncludes(int id)
        {
            return _articleDal.GetArticleByIdWithIncludes(id);
        }

        public Article TGetArticleBySlug(string slug)
        {
            return _articleDal.Get(a => a.Slug == slug);
        }
    }
}
