using BlogStore.BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace BlogStore.PresentationLayer.ViewComponents
{
    public class _NavbarLayoutComponentPartial:ViewComponent
    {
        private readonly ICategoryService _categoryService;

        public _NavbarLayoutComponentPartial(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IViewComponentResult Invoke()
        {
            var categories = _categoryService.TGetAll();
            return View(categories);
        }
    }
}
