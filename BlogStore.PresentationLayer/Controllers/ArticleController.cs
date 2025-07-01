using BlogStore.BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace BlogStore.PresentationLayer.Controllers
{
    public class ArticleController : Controller
    {
        private readonly ICommentService _commentService;

        public ArticleController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        public IActionResult ArticleDetail(int id)
        {
            ViewBag.i = id;
            return View();
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
    }
}
