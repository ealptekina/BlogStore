using BlogStore.BusinessLayer.Abstract;
using BlogStore.EntityLayer.Entities;
using BlogStore.PresentationLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogStore.PresentationLayer.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IArticleService _articleService;
        private readonly ICategoryService _categoryService;
        private readonly ICommentService _commentService;

        public DashboardController(IArticleService articleService, ICategoryService categoryService, ICommentService commentService)
        {
            _articleService = articleService;
            _categoryService = categoryService;
            _commentService = commentService;
        }

        public IActionResult Index()
        {

            // Yazarların makale sayıları
            var articles = _articleService.TGetListWithCategoryAndUser();

            var authorData = articles
                .Where(a => a.AppUser != null)
                .GroupBy(a => a.AppUser.Name + " " + a.AppUser.Surname)
                .Select(g => new AuthorArticleWithCategoriesViewModel
                {
                    UserName = g.Key,
                    ArticleCount = g.Count(),
                    Categories = g.Where(x => x.Category != null)
                                  .Select(x => x.Category.CategoryName)
                                  .Distinct()
                                  .ToList()
                })
                .ToList();



            // Son 5 makale

            var last5Articles = _articleService.TGetListWithCategoryAndUser()
                                              .OrderByDescending(x => x.CreatedDate)
                                              .Take(5)
                                              .ToList();

            // En çok yorum alan makale

            var mostCommentedArticle = _articleService.TGetAll()
                .OrderByDescending(a => _commentService.TGetCommentsByArticle(a.ArticleId).Count())
                .FirstOrDefault();

            // Kategori istatistikleri
            var articleCountsByCategory = _articleService.TGetAll()
                .Where(a => a.Category != null && !string.IsNullOrEmpty(a.Category.CategoryName))
                .GroupBy(a => a.Category.CategoryName)
                .Select(g => new CategoryStatViewModel { CategoryName = g.Key, Count = g.Count() })
                .ToList();

            // Toplam sayılar
            var categoryCount = _categoryService.TGetAll().Count();
            var articleCount = _articleService.TGetAll().Count();
            var commentCount = _commentService.TGetAll().Count();

            // ViewModel'i doldur
            var model = new DashboardViewModel
            {
                Last5Articles = last5Articles,
                MostCommentedArticle = mostCommentedArticle,
                CategoryStats = articleCountsByCategory,
                CategoryCount = categoryCount,
                ArticleCount = articleCount,
                CommentCount = commentCount,
                AuthorArticleWithCategories = authorData
            };

            return View(model);
        }

    }



}
