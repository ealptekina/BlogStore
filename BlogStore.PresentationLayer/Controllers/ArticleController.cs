using Microsoft.AspNetCore.Mvc;

namespace BlogStore.PresentationLayer.Controllers
{
    public class ArticleController : Controller
    {
        public IActionResult ArticleDetail(int id)
        {
            return View();
        }
    }
}
