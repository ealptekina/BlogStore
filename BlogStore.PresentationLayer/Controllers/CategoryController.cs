using BlogStore.BusinessLayer.Abstract;
using BlogStore.EntityLayer.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BlogStore.PresentationLayer.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IArticleService _articleService;

        public CategoryController(ICategoryService categoryService, IArticleService articleService)
        {
            _categoryService = categoryService;
            _articleService = articleService;
        }

        public IActionResult CategoryList()
        {
            var values = _categoryService.TGetAll();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateCategory(Category category)
        {
            _categoryService.TInsert(category);
            return RedirectToAction("CategoryList");
        }

        public IActionResult DeleteCategory(int id)
        {
            _categoryService.TDelete(id);
            return RedirectToAction("CategoryList");
        }

        [HttpGet]
        public IActionResult UpdateCategory(int id)
        {
            var value = _categoryService.TGetById(id);
            return View(value);
        }

        [HttpPost]
        public IActionResult UpdateCategory(Category category)
        {
            _categoryService.TUpdate(category);
            return RedirectToAction("CategoryList");
        }

        public IActionResult AdminCategoryList()
        {
            var values = _categoryService.TGetAll();
            return View(values);
        }

        // Blog menüsünden gelen tüm kategorileri listeler
        public IActionResult Index(int page = 1)
        {
            int pageSize = 3; // Her sayfada 6 kategori

            var allCategories = _categoryService.TGetAll().ToList();

            // Sayfalama işlemi
            var pagedCategories = allCategories
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling(allCategories.Count / (double)pageSize);

            return View(pagedCategories);
        }



        // Belirli kategoriye ait makaleleri getirir
        public IActionResult CategoryArticles(string categorySlug, int page = 1)
        {
            int pageSize = 2; // Her sayfada 3 makale

            if (string.IsNullOrEmpty(categorySlug)) return NotFound();

            var category = _categoryService.TGetAll()
                            .FirstOrDefault(x => x.CategoryName.ToLower().Replace(" ", "-") == categorySlug);

            if (category == null) return NotFound();

            var allArticles = _articleService.TGetArticlesWithCategory()
                                .Where(x => x.CategoryId == category.CategoryId)
                                .ToList();

            var pagedArticles = allArticles
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling(allArticles.Count / (double)pageSize);
            ViewBag.CategoryName = category.CategoryName;
            ViewBag.CategorySlug = categorySlug;

            return View(pagedArticles);
        }

    }
}