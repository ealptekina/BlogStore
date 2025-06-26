using BlogStore.BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace BlogStore.PresentationLayer.ViewComponents.ArticleDetailViewComponents
{
    public class _ArticleDetailPopularPostComponentPartial:ViewComponent
    {
        private readonly IArticleService _articleService;

        public _ArticleDetailPopularPostComponentPartial(IArticleService articleService)
        {
            _articleService = articleService;
        }

        public IViewComponentResult Invoke()
        {
            var values = _articleService.TGetTop3PopularArticles();
            return View(values);
        }
    }
}
