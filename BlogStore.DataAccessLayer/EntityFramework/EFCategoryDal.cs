using BlogStore.DataAccessLayer.Abstract;
using BlogStore.DataAccessLayer.Context;
using BlogStore.DataAccessLayer.Dtos;
using BlogStore.DataAccessLayer.Repositories;
using BlogStore.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogStore.DataAccessLayer.EntityFramework
{
    public class EFCategoryDal : GenericRepository<Category>, ICategoryDal
    {
        private readonly BlogContext _context;

        public EFCategoryDal(BlogContext context) : base(context)
        {
            _context = context;
        }

        public List<CategoryWithArticleCountDto> GetCategoryWithArticleCount()
        {
            var result = _context.Categories.Select(c => new CategoryWithArticleCountDto
            {
                CategoryName = c.CategoryName, // Kategori adı
                ArticleCount = _context.Articles.Count(a => a.CategoryId == c.CategoryId) // Makale sayısı
            }).ToList();

            return result;
        }
    }
}
