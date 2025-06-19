using BlogStore.BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace BlogStore.PresentationLayer.ViewComponents.ArticleDetailViewComponents
{
    public class _ArticleDetailCategoryComponentPartial:ViewComponent
    {
        private readonly ICategoryService _categoryService;

        public _ArticleDetailCategoryComponentPartial(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IViewComponentResult Invoke()
        {
            var values = _categoryService.TGetCategoryWithArticleCount();
            return View(values);
        }
    }
}
