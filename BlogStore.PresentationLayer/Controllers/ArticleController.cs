using BlogStore.BusinessLayer.Abstract;
using BlogStore.EntityLayer.Entities;
using BlogStore.PresentationLayer.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace BlogStore.PresentationLayer.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IArticleService _articleService;  // Ekle
        private readonly ICommentService _commentService;

        public ArticleController(IArticleService articleService, ICommentService commentService)
        {
            _articleService = articleService;  // Ata
            _commentService = commentService;
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

        public IActionResult ArticleList(int id)
        {
            //var values = 
            return View();
        }

        public IActionResult GetCommentsByArticle(int articleId)
        {
            var comments = _commentService.TGetCommentsByArticle(articleId);
            return PartialView("_ArticleDetailCommentListComponentPartial", comments);
        }

        [HttpPost]
        public IActionResult CreateArticle(Article model)
        {
            if (ModelState.IsValid)
            {
                model.Slug = SlugHelper.GenerateSlug(model.Title);
                _articleService.TInsert(model);
                return RedirectToAction("MyArticleList");
            }

            return View(model);
        }
    }
}
