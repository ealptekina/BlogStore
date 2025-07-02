using BlogStore.BusinessLayer.Abstract;
using BlogStore.EntityLayer.Entities;
using BlogStore.PresentationLayer.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlogStore.PresentationLayer.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IArticleService _articleService;  
        private readonly ICommentService _commentService;
        private readonly ICategoryService _categoryService;

        public ArticleController(IArticleService articleService, ICommentService commentService, ICategoryService categoryService)
        {
            _articleService = articleService;
            _commentService = commentService;
            _categoryService = categoryService;
        }

        [Route("Article/ArticleDetail/{slug}")]
        public IActionResult ArticleDetail(string slug)
        {
            if (string.IsNullOrEmpty(slug))
                return NotFound();

            var article = _articleService.TGetArticleBySlug(slug);
            if (article == null)
                return NotFound();

            ViewBag.i = article.ArticleId;
            return View(article);
        }

        // Listeleme
        public IActionResult ArticleList(int id)
        {
            var articles = _articleService.TGetListWithCategoryAndUser();
            return View(articles);
        }

        public IActionResult GetCommentsByArticle(int articleId)
        {
            var comments = _commentService.TGetCommentsByArticle(articleId);
            return PartialView("_ArticleDetailCommentListComponentPartial", comments);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(_categoryService.TGetAll(), "CategoryId", "CategoryName");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Article article, IFormFile ImageFile)
        {
            if (ModelState.IsValid)
            {
                if (ImageFile != null && ImageFile.Length > 0)
                {
                    var fileName = Guid.NewGuid() + Path.GetExtension(ImageFile.FileName);
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", fileName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await ImageFile.CopyToAsync(stream);
                    }

                    article.ImageUrl = "/uploads/" + fileName;
                }

                article.CreatedDate = DateTime.Now;
                _articleService.TInsert(article);
                return RedirectToAction("ArticleList");
            }

            ViewBag.Categories = new SelectList(_categoryService.TGetAll(), "CategoryId", "CategoryName");
            return View(article);
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            var article = _articleService.TGetById(id);
            if (article == null) return NotFound();

            ViewBag.Categories = new SelectList(_categoryService.TGetAll(), "CategoryId", "CategoryName", article.CategoryId);
            return View(article);
        }

        [HttpPost]
        public IActionResult Edit(Article article)
        {
            if (ModelState.IsValid)
            {
                _articleService.TUpdate(article);
                return RedirectToAction("Index");
            }
            ViewBag.Categories = new SelectList(_categoryService.TGetAll(), "CategoryId", "CategoryName", article.CategoryId);
            return View(article);
        }

        public IActionResult Delete(int id)
        {
            var article = _articleService.TGetById(id);
            if (article == null) return NotFound();

            _articleService.TDelete(article.ArticleId);
            return RedirectToAction("Index");
        }

    }
}
