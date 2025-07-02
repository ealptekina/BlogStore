using BlogStore.BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace BlogStore.PresentationLayer.ViewComponents.ArticleDetailViewComponents
{
    public class _ArticleDetailTagComponentPartial:ViewComponent
    {
        private readonly ITagService _tagService;

        public _ArticleDetailTagComponentPartial(ITagService tagService)
        {
            _tagService = tagService;
        }

        public IViewComponentResult Invoke(int id)
        {
            var tags = _tagService.GetTagsByArticleId(id); // Makaleye ait etiketleri getir
            return View(tags); // List<Tag> gönder
        }
    }
}
